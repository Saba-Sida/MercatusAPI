using Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services;

public class InMemoryCacheService: ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    public Task SetValueByKey<T>(string key, T value, TimeSpan timeToLive)
    {
        _memoryCache.Set(key, value, timeToLive);
        return Task.CompletedTask;
    }

    public Task<T?> GetValueByKey<T>(string key)
    {
        return Task.FromResult(_memoryCache.Get<T>(key));
    }
}