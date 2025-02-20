namespace Solution1.Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string CourseName { get; set; }
    public string Department { get; set; }
    public List<Student> students { get; set; }
    public List<Teacher> teachers { get; set; }
}