namespace KIT.Redis.Interfaces;

public interface IRedisRepository
{
    Task<T?> GetValueAsync<T>(string key) where T : struct;

    Task<T> GetAsync<T>(string key) where T : class;

    Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null);

    Task<bool> ExpireAsync(string key, TimeSpan? expiry);

    Task<bool> DeleteAsync(string key);

    Task<double> IncrementValueAsync(string key, double value);

    Task<double> DecrementValueAsync(string key, double value);
}