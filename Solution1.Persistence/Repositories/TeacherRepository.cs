using Microsoft.EntityFrameworkCore;
using Solution1.Domain.Entities;
using Solution1.Persistence.Cache;
using Solution1.Persistence.Database;

namespace Solution1.Persistence.Repositories;

public class TeacherRepository
{
    private readonly UniversityDbContext _universityDbContext;
    private readonly RedisCacheService _redisCacheService;

    public TeacherRepository(UniversityDbContext universityDbContext, RedisCacheService redisCacheService)
    {
        _universityDbContext = universityDbContext;
        _redisCacheService = redisCacheService;
    }
    

    public async Task<Teacher> GetById(int id)
    {
        string key = $"teacher:{id}";
        var cached = await _redisCacheService.GetAsync<Teacher>(key);
        if (cached != null) return cached;
        var teacher = await _universityDbContext.Teachers.FindAsync(id);
        if (teacher == null) throw new ArgumentNullException("Teacher not found");
        await _redisCacheService.SetAsync(key, teacher, new TimeSpan(0, 30, 0));
        return teacher;
    }

    public async Task<List<Teacher>> GetTeachers()
    {
        string key = "teachers:all";
        var cached = await _redisCacheService.GetAsync<List<Teacher>>(key);
        if (cached != null) return cached;
        var teachers = await _universityDbContext.Teachers.ToListAsync();
        await _redisCacheService.SetAsync(key, teachers, new TimeSpan(0, 30, 0));
        return teachers;
    }

    public async Task<Teacher> Add(Teacher teacher)
    {
        _universityDbContext.Teachers.Add(teacher);
        return teacher;
        
    }

    public async Task Grade(int StudentId,int grade)//out of 20
    {
        var s = await _universityDbContext.Students.FindAsync(StudentId);
        if(s==null) throw new ArgumentNullException("Student not found");
        s.Grade = (s.Grade + 20)/2;
        s.CanApplyToFrance = s.Grade > 15?true:false;
        await _universityDbContext.SaveChangesAsync();
    }
    public async Task<Teacher> Delete(int id)
    {
        var teacher = await _universityDbContext.Teachers.FindAsync(id);
        _universityDbContext.Teachers.Remove(teacher);
        return teacher;
    }
    public async Task SaveChanges()
    {
        await _universityDbContext.SaveChangesAsync();
    }

    
    
}