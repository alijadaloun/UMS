using System.ComponentModel.DataAnnotations;

namespace Solution1.Domain.Entities;

public class Student
{
    [Key]
    public int Id { get; set; }
    public string StudentName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Course> Courses { get; set; }
}