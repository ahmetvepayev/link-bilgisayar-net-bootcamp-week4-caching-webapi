using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace CacheWebApi.Caching;

public static class CacheConfigure
{
    public enum CachingType
    {
        InMemoryAspNet,
        DistributedRedis
    }
    
    public static void AddCachingService(this WebApplicationBuilder builder, CachingType cachingType)
    {
        switch (cachingType)
        {
            case CachingType.InMemoryAspNet :
            {
                builder.Services.AddMemoryCache();
                builder.Services.AddScoped<ICaching, InMemoryCaching>();
                
                break;
            }
            case CachingType.DistributedRedis :
            {
                builder.Services.AddScoped<ICaching, RedisCaching>();
                builder.Services.AddSingleton<IConnectionMultiplexer>(options => 
                    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisCon"))
                );
                
                break;
            }
        }
    }
}