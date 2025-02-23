using Microsoft.EntityFrameworkCore;
using Solution1.Domain.Entities;
using Solution1.Persistence.Cache;
using Solution1.Persistence.Database;

namespace Solution1.Persistence.Repositories;

public class StudentRepository
{
    private readonly UniversityDbContext _universityDbContext;
    private readonly RedisCacheService _redisCacheService;

    public StudentRepository(UniversityDbContext universityDbContext, RedisCacheService redisCacheService)
    {
        _universityDbContext = universityDbContext;
        _redisCacheService = redisCacheService;
    }
    public async Task<Student> GetById(int id)
    {
        string key = $"student:{id}";
        var cached = await _redisCacheService.GetAsync<Student>(key);
        if (cached != null) return cached;
        var student = await _universityDbContext.Students.FindAsync(id);
        if (student==null) throw new ArgumentNullException("Student not found");
        await _redisCacheService.SetAsync(key, student, new TimeSpan(0, 30, 0));
        return  student;

    }

   public  async Task<List<Student>>GetAll()
    {
        string key = "students:all";
        var cached = await _redisCacheService.GetAsync<List<Student>>(key);
        if (cached != null) return cached;
        var students = await _universityDbContext.Students.ToListAsync();
        await _redisCacheService.SetAsync(key, students, new TimeSpan(0, 30, 0));
        return  await _universityDbContext.Students.ToListAsync();
    }

    public async Task<Student> RegisterStudent(int studentId, int  courseId,int teacherId)
    {
        var student = await _universityDbContext.Students.FindAsync(studentId);
        if (student == null) throw new ArgumentNullException("Student not found");
        if (await _universityDbContext.Courses.FindAsync(courseId)==null )throw new ArgumentNullException("Course not found");
        var course = await _universityDbContext.Courses.FindAsync(courseId);
        // if (course == null) throw new ArgumentNullException("Course not found");
        // var teacher = await _universityDbContext.Teachers.FindAsync(teacherId);
        // if (teacher == null) throw new ArgumentNullException("Teacher not found");
         _universityDbContext.Students.Find(studentId).Courses.Add(course);
        
        return student;
        
        
    }

    public async Task<int> Add(Student student)
    {
         await _universityDbContext.Students.AddAsync(student);
        return student.Id;
    }

    public async Task<Student> Delete(int id)
    {
        var student = await _universityDbContext.Students.FindAsync(id);
        _universityDbContext.Students.Remove(student);
        return student;
    }

    public async Task SaveChanges()
    {
        await _universityDbContext.SaveChangesAsync();
    }
}