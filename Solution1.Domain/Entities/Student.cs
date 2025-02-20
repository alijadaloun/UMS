namespace Solution1.Domain.Entities;

public class Student
{
    public int Id { get; set; }
    public string StudentName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Course> courses { get; set; }
}