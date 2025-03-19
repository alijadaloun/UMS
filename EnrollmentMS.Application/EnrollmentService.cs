using EnrollmentMS.Domain;
using EnrollmentMS.Persistence;

namespace EnrollmentMS.Application;

public class EnrollmentService: IEnrollmentService
{
    private readonly EnrollmentRepository _enrollmentRepository;

    public EnrollmentService(EnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public Task<Enrollment> GetEnrollmentById(int enrollmentId)
    {
        return _enrollmentRepository.GetEnrollmentByIdAsync(enrollmentId);
    }

    public Task<List<Enrollment>> GetEnrollmentsByStudentId(int studentId)
    {
        return _enrollmentRepository.GetEnrollmentsByStudentIdAsync(studentId);
    }

    public Task<List<Enrollment>> GetEnrollmentsByCourseId(int courseId)
    {
        return _enrollmentRepository.GetEnrollmentsByCourseIdAsync(courseId);
    }

    public Task<List<Enrollment>> GetEnrollments()
    {
        return _enrollmentRepository.GetEnrollmentsAsync();
    }


    public async Task Enroll(Enrollment enrollment)
    {
        var studentid = enrollment.StudentId;
        var courseId = enrollment.CourseId;
        await _enrollmentRepository.EnrollAsync(studentid, courseId);
        
    }

    public async Task RemoveEnrollment(Enrollment enrollment)
    {
        await _enrollmentRepository.RemoveEnrollmentAsync(enrollment);
    }
}