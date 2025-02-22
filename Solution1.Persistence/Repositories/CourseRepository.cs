using Microsoft.EntityFrameworkCore;
using Solution1.Domain.Entities;
using Solution1.Persistence.Database;

namespace Solution1.Persistence.Repositories;

public class CourseRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public CourseRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<Course> Get(int id)
    {
        return await _universityDbContext.Courses.FindAsync(id);
    }

    public async Task<List<Course>> GetAll()
    {
        var courses =  await _universityDbContext.Courses.ToListAsync(); 
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