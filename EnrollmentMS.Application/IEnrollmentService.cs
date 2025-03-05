using EnrollmentMS.Domain;

namespace EnrollmentMS.Application;

public interface IEnrollmentService
{
    public Task<Enrollment> GetEnrollmentById(int enrollmentId);
    public  Task<List<Enrollment>> GetEnrollmentsByStudentId(int studentId);
    public Task<List<Enrollment>> GetEnrollmentsByCourseId(int courseId);
    public Task<List<Enrollment>> GetEnrollments();
    public Task Enroll(Enrollment enrollment);
    public Task RemoveEnrollment(Enrollment enrollment);
}