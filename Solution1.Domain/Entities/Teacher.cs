namespace Solution1.Domain.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string TeacherName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public List<Course> Courses { get; set; }

}