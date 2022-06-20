namespace CacheWebApi.Caching;

public class CacheOptions
{
    public string Key { get; set; }
    public int AbsoluteExpiration { get; set; }
    public int SlidingExpiration { get; set; }

    public TimeSpan absoluteLifetime => TimeSpan.FromMinutes(AbsoluteExpiration);
    public TimeSpan slidingLifetime => TimeSpan.FromMinutes(AbsoluteExpiration);
}