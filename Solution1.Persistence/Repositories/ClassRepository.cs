using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solution1.Domain.Entities;
using Solution1.Infrastructure.Cache;
using Solution1.Persistence.Database;

namespace Solution1.Persistence.Repositories;

public class ClassRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public ClassRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;

    }

    public async Task<Class> Get(int id)
    {
        var c = await _universityDbContext.Classes.FindAsync(id);
        if (c == null) throw new ArgumentException("Class not found");
        return c;
    }

    public async Task<List<Class>> GetAll()
    {
        
        var classes = await _universityDbContext.Classes.ToListAsync();
        
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