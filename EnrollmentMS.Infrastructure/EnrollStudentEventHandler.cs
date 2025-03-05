using EnrollmentMS.Persistence;
using MassTransit;
using Shared;

namespace EnrollmentMS.Infrastructure;

public class EnrollStudentEventHandler : IConsumer<StudentCreatedEvent>
{
    private readonly EnrollmentRepository _enrollmentRepository;

    public EnrollStudentEventHandler(EnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task Consume(ConsumeContext<StudentCreatedEvent> context)
    {
        var studentId = context.Message.StudentId;
        var courseId = context.Message.CourseId;

        // Check if the student is already enrolled
        if (await _enrollmentRepository.GetEnrollmentsByStudentIdAsync(studentId) == await _enrollmentRepository.GetEnrollmentsByCourseIdAsync(courseId))
        {
            Console.WriteLine($"Student {studentId} is already enrolled in Course {courseId}.");
            return;
        }

        // Enroll the student in the course
        await _enrollmentRepository.EnrollAsync(studentId, courseId);

        Console.WriteLine($"Student {studentId} is enrolled in the course of id: {courseId}.");
    }
}