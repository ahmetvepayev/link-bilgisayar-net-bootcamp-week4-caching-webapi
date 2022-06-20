namespace CacheWebApi.Caching;

public interface ICaching
{
    void Set(string key, object value);
    void Set(string key, object value, TimeSpan absoluteExpiration, TimeSpan slidingExpiration);
    T Get<T>(string key);
    bool TryGet<T>(string key, out T value);
    void Remove(string key);
}