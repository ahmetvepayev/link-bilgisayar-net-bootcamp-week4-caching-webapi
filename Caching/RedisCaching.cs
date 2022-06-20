using System.Text.Json;
using StackExchange.Redis;

namespace CacheWebApi.Caching;

public class RedisCaching : ICaching
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;

    public RedisCaching(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = _redis.GetDatabase();
    }

    public void Set(string key, object value)
    {
        var serialized = JsonSerializer.Serialize(value);
        _db.StringSet(key, serialized);
    }

    // Sliding expiration not implemented
    public void Set(string key, object value, TimeSpan absoluteExpiration, TimeSpan slidingExpiration)
    {
        var serialized = JsonSerializer.Serialize(value);
        _db.StringSet(key, serialized, absoluteExpiration);
    }

    public T Get<T>(string key)
    {
        var value = _db.StringGet(key);

        return JsonSerializer.Deserialize<T>(value);
    }

    public bool TryGet<T>(string key, out T value)
    {
        var redisValue = _db.StringGet(key);
        if (!String.IsNullOrEmpty(redisValue))
        {
            value = JsonSerializer.Deserialize<T>(redisValue);
            return true;
        }
        else
        {
            value = default(T);
            return false;
        }
    }

    public void Remove(string key)
    {
        _db.KeyDelete(key);
    }
}