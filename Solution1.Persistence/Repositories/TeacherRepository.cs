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
       
        var teacher = await _universityDbContext.Teachers.FindAsync(id);
        if (teacher == null) throw new ArgumentNullException("Teacher not found");
        return teacher;
    }

    public async Task<List<Teacher>> GetTeachers()
    {
       
        var teachers = await _universityDbContext.Teachers.ToListAsync();
        return teachers;
    }

    public async Task<Teacher> Add(Teacher teacher)
    {
        await _universityDbContext.Teachers.AddAsync(teacher);
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
    public string GradeBackground(int StudentId,int grade)//out of 20
    {
        var s =   _universityDbContext.Students.Find(StudentId);
        if(s==null) throw new ArgumentNullException("Student not found");
        s.Grade = (s.Grade + 20)/2;
        s.CanApplyToFrance = s.Grade > 15?true:false; 
        var email = SendEmail(StudentId);
        return email;
    }

    private string SendEmail(int studentId)
    {
        var s =  _universityDbContext.Students.Find(studentId);
        if(s==null) throw new ArgumentNullException("Student not found");
        string email = s.Email;//Email will be send in this for student from the hangfire method daily
        return email;

    }

    public string Notify(int studentId, string deadline)
    {
        var s = SendEmail(studentId);
        return s;
        
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