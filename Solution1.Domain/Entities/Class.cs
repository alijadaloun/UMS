using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution1.Domain.Entities;

public class Class
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Semester { get; set; }
    public int Year { get; set; }
    public string Room { get; set; }
    public List<Course> Courses { get; set; } =  new List<Course>();

}