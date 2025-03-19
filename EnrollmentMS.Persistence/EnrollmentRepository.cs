using EnrollmentMS.Domain;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EnrollmentMS.Persistence;

public class EnrollmentRepository
{
    private readonly EnrollmentDbContext _context;
    private readonly IEventBus _eventBus;
    
    
    public EnrollmentRepository( EnrollmentDbContext context, IEventBus eventBus )
    {
        _context = context;
        _eventBus = eventBus;
        
    }


    public async Task<List<Enrollment>> GetEnrollmentsAsync()
    {
        var enrollments = await _context.Enrollments.ToListAsync();
        return enrollments;
        
    }

    public async Task<List<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId)
    {
        var enrollments = await _context.Enrollments.Where(e => e.CourseId == courseId).ToListAsync();
        return enrollments;
    }

    public async Task<List<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId)
    {
        var enrollments = await _context.Enrollments.Where(e => e.StudentId == studentId).ToListAsync();
        return enrollments;
    }

    public async Task<Enrollment> GetEnrollmentByIdAsync(int enrollmentId)
    {
        var enrollment = await _context.Enrollments.FindAsync(enrollmentId);
        if(enrollment == null)throw new Exception("Enrollment not found");
        return enrollment;
    }

    public async Task EnrollAsync(int studentId, int courseId)
    {
        if( !await _context.Enrollments.AnyAsync(e => e.StudentId == studentId || e.CourseId == courseId))throw new Exception("Student or Course not found");
        _eventBus.Subscribe<StudentCreatedEvent,IIntegrationEventHandler<StudentCreatedEvent>>();
        //here we are registring the student into the message queue in rabbitMQ to execute the enroll function from the microservice
        
        
        await _context.SaveChangesAsync();
    }

    public async Task RemoveEnrollmentAsync(Enrollment enrollment)
    {
        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();
    }

}