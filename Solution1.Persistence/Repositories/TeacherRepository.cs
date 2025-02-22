using Microsoft.EntityFrameworkCore;
using Solution1.Domain.Entities;
using Solution1.Persistence.Database;

namespace Solution1.Persistence.Repositories;

public class TeacherRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public TeacherRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<Teacher> GetById(int id)
    {
        if (await _universityDbContext.Teachers.FindAsync(id) == null) throw new ArgumentNullException("Teacher not found");
        var teacher = await _universityDbContext.Teachers.FindAsync(id);
        return teacher;
    }

    public async Task<List<Teacher>> GetTeachers()
    {
        return await _universityDbContext.Teachers.ToListAsync();
    }

    public async Task<Teacher> Add(Teacher teacher)
    {
        _universityDbContext.Teachers.Add(teacher);
        return teacher;
        
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