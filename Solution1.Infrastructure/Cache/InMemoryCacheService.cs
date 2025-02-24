using Microsoft.Extensions.Caching.Memory;
namespace Solution1.Infrastructure.Cache;

public class InMemoryCacheService
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T GetOrAdd<T>(string key, Func<T> addItem, TimeSpan expiry)
    {
        if (!_memoryCache.TryGetValue(key, out T value))
        {
            value = addItem();
            _memoryCache.Set(key, value, expiry);
            
        }
        return value;
        
    }
}