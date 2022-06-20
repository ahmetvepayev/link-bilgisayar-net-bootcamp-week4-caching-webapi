using Microsoft.Extensions.Caching.Memory;

namespace CacheWebApi.Caching;

public class InMemoryCaching : ICaching
{
    private readonly IMemoryCache _cache;

    public InMemoryCaching(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Set(string key, object value)
    {
        _cache.Set(key, value);
    }

    public void Set(string key, object value, TimeSpan absoluteExpiration, TimeSpan slidingExpiration)
    {
        if (absoluteExpiration <= TimeSpan.Zero)
        {
            Set(key, value);
            return;
        }

        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpiration
        };
        if (slidingExpiration > TimeSpan.Zero && slidingExpiration < absoluteExpiration)
        {
            options.SlidingExpiration = slidingExpiration;
        }

        _cache.Set(key, value, options);
    }

    public T Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    public bool TryGet<T>(string key, out T value)
    {
        return _cache.TryGetValue<T>(key, out value);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}