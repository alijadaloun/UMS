namespace Solution1.Domain.Entities;

public class Class
{
    public int Id { get; set; }
    public string Semester { get; set; }
    public int Year { get; set; }
    public string Room { get; set; }
    public List<Course> Courses { get; set; }

}