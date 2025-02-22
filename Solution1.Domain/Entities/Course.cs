namespace Solution1.Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string CourseName { get; set; }
    public string Department { get; set; }
    public int classId { get; set; }
    public Class Class { get; set; }
    public List<Student> Students { get; set; }
    public List<Teacher> Teachers { get; set; }
    
}