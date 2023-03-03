using System.Globalization;
using System.Text.Json;
using KIT.Redis.Interfaces;
using StackExchange.Redis;

namespace KIT.Redis.Repositories;

public class RedisRepository : IRedisRepository, IDisposable
{
    private readonly IConnectionMultiplexer _connection;
    private readonly string _prefix;

    private bool _disposed;

    public RedisRepository(IRedisSettings settings)
    {
        _connection = ConnectionMultiplexer.Connect(settings.RedisConnectionString);
        _prefix = settings.RedisPrefix;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<T?> GetValueAsync<T>(string key) where T : struct
    {
        var database = _connection.GetDatabase();
        var value = await database.StringGetAsync(GetKeyWithPrefix(key));

        if (!value.HasValue) return null;

        if (typeof(T) == typeof(Guid)) return (T)(object)Guid.Parse(value);

        if (typeof(T) == typeof(DateTime)) return (T)(object)DateTime.Parse(value);

        if (typeof(T).IsPrimitive) return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);

        return JsonSerializer.Deserialize<T>(value);
    }

    public async Task<T> GetAsync<T>(string key) where T : class
    {
        var database = _connection.GetDatabase();
        var value = await database.StringGetAsync(GetKeyWithPrefix(key));

        return value.HasValue ? JsonSerializer.Deserialize<T>(value) : null;
    }

    public Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        if (value == null) throw new ArgumentNullException(nameof(value));

        var strValue = value switch
        {
            IConvertible convertible => convertible.ToString(CultureInfo.InvariantCulture),
            Guid id => id.ToString(),
            _ => JsonSerializer.Serialize(value)
        };

        var database = _connection.GetDatabase();
        return database.StringSetAsync(GetKeyWithPrefix(key), strValue, expiry);
    }

    public Task<bool> ExpireAsync(string key, TimeSpan? expiry)
    {
        var database = _connection.GetDatabase();
        return database.KeyExpireAsync(GetKeyWithPrefix(key), expiry);
    }

    public Task<bool> DeleteAsync(string key)
    {
        var database = _connection.GetDatabase();
        return database.KeyDeleteAsync(GetKeyWithPrefix(key));
    }

    public Task<double> IncrementValueAsync(string key, double value)
    {
        var database = _connection.GetDatabase();
        return database.StringIncrementAsync(GetKeyWithPrefix(key), value);
    }

    public Task<double> DecrementValueAsync(string key, double value)
    {
        var database = _connection.GetDatabase();
        return database.StringDecrementAsync(GetKeyWithPrefix(key), value);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            _connection.Close();
            _connection.Dispose();
        }

        _disposed = true;
    }

    private string GetKeyWithPrefix(string key)
    {
        return $"{_prefix}.{key}";
    }
}