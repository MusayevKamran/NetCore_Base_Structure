namespace KIT.Redis.Interfaces;

public interface IRedisSettings
{
    string RedisConnectionString { get; }

    string RedisPrefix { get; }
}