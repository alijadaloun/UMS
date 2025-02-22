using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution1.Domain.Entities;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string StudentName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Course> Courses { get; set; } =  new List<Course>();
}