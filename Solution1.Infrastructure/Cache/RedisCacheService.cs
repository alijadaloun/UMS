using Newtonsoft.Json;
using StackExchange.Redis;


namespace Solution1.Infrastructure.Cache;

public class RedisCacheService
{
    private readonly IDatabase _redis;

    public RedisCacheService(IConnectionMultiplexer database)
    {
        _redis = database.GetDatabase();
        
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _redis.StringGetAsync(key);
        return  value.HasValue? JsonConvert.DeserializeObject<T>(value): default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiry)
    {
        var json =  JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings(){ ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        await _redis.StringSetAsync(key, json, expiry);
        
    }

    public async Task RemoveAsync(string key)
    {
        await _redis.KeyDeleteAsync(key);
    }

    
    
}