using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution1.Domain.Entities;

public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string CourseName { get; set; }
    public string Department { get; set; }
    public int classId { get; set; } = 6;
    public Class Class { get; set; } = new Class();
    public List<Student> Students { get; set; } = new List<Student>();
    public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    
}