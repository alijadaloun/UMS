using Microsoft.EntityFrameworkCore;
using Solution1.Domain.Entities;
using Solution1.Persistence.Cache;
using Solution1.Persistence.Database;

namespace Solution1.Persistence.Repositories;

public class CourseRepository
{
    private readonly UniversityDbContext _universityDbContext;
    private readonly RedisCacheService _redisCacheService;

    public CourseRepository(UniversityDbContext universityDbContext, RedisCacheService redisCacheService)
    {
        _universityDbContext = universityDbContext;
        _redisCacheService = redisCacheService;
    }

    public async Task<Course> Get(int id)
    {
        string key = $"course:{id}";
        var cached = await _redisCacheService.GetAsync<Course>(key);
        if (cached != null) return cached;
        var course = await _universityDbContext.Courses.FindAsync(id);
        await _redisCacheService.SetAsync(key, course, new TimeSpan(0, 30, 0));
        return course ;
    }

    public async Task<List<Course>> GetAll()
    {
        string key = "courses:all";
        var cached = await _redisCacheService.GetAsync<List<Course>>( key);
        if (cached != null) return cached;
        var courses =  await _universityDbContext.Courses.ToListAsync(); 
        await _redisCacheService.SetAsync(key, courses, new TimeSpan(0, 30, 0));   
        return courses;
    }
    public async Task<Course> Add(Course course)
    {
        await _universityDbContext.Courses.AddAsync(course);
        return course;
        
    }

    public async Task<Course> Delete(int id)
    {
        var course = await _universityDbContext.Courses.FindAsync(id);
        if (course == null) throw new ArgumentNullException("No courses found");
        _universityDbContext.Courses.Remove(course);
        return course;
    }
    public async Task SaveChanges()
    {
        await _universityDbContext.SaveChangesAsync();
    }
}