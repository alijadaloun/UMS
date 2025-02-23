using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solution1.Domain.Entities;
using Solution1.Persistence.Cache;
using Solution1.Persistence.Database;

namespace Solution1.Persistence.Repositories;

public class ClassRepository
{
    private readonly UniversityDbContext _universityDbContext;
    private readonly RedisCacheService _redisCacheService;
    private readonly ILogger<ClassRepository> _logger;

    public ClassRepository(UniversityDbContext universityDbContext, RedisCacheService redisCacheService, ILogger<ClassRepository> logger)
    {
        _universityDbContext = universityDbContext;
        _redisCacheService = redisCacheService;
        _logger = logger;

    }

    public async Task<Class> Get(int id)
    {
        string cacheKey = $"class:{id}";
        var cached = await _redisCacheService.GetAsync<Class>(cacheKey);
        if (cached != null) return cached;
        var c = await _universityDbContext.Classes.FindAsync(id);
        await _redisCacheService.SetAsync(
            cacheKey, 
            c,
            new TimeSpan(0, 30, 0)
        );
        return c;
    }

    public async Task<List<Class>> GetAll()
    {
        string cacheKey = $"classes:all";
        var cached = await _redisCacheService.GetAsync<List<Class>>(cacheKey);
        if (cached != null) return cached;
        
        var classes = await _universityDbContext.Classes.ToListAsync();
        await _redisCacheService.SetAsync(
            cacheKey, 
            classes,
            new TimeSpan(0, 30, 0)
        );
        
        return classes;
    }

    public async Task<int> Add(Class c)
    {
        await _universityDbContext.Classes.AddAsync(c);
        await _universityDbContext.SaveChangesAsync();
        return c.Id;
    }

    public async Task<int> Delete(int id)
    {
        var c = await _universityDbContext.Classes.FindAsync(id);
        if(c==null) return 0;
        _universityDbContext.Classes.Remove(c);
        
        
        return id;
    }
    public async Task SaveChanges()
    {
        await _universityDbContext.SaveChangesAsync();
    }
    
    
}