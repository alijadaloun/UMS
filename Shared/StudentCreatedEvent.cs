namespace Shared;

public class StudentCreatedEvent
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public bool  EnrollStudentEvent(int studentId, int courseId)
    {
        StudentId = studentId;
         CourseId= courseId;
         return true;
         
    }
}