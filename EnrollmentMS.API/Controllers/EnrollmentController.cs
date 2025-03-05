using EnrollmentMS.Application;
using EnrollmentMS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentMS.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EnrollmentController:ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<Enrollment>>> GetEnrollments()
    {
        var results = await _enrollmentService.GetEnrollments();
        return Ok(results);
    }
[HttpGet("get/course/{courseId}")]
    public async Task<ActionResult<List<Enrollment>>> GetEnrollmentsByCourseId(int courseId)
    {
        var results =  await _enrollmentService.GetEnrollmentsByCourseId(courseId);
        return Ok(results);
        
    }
    [HttpGet("get/student/{studentId}")]

    public async Task<ActionResult<List<Enrollment>>> GetEnrollmentsByStudentId(int studentId)
    {
        var results = await _enrollmentService.GetEnrollmentsByStudentId(studentId);
        return Ok(results);
    }
    
    [HttpGet("get/enrollment/{enrollmentId}")]
    public async Task<ActionResult<Enrollment>> GetEnrollmentById(int enrollmentId)
    {
        var results = await _enrollmentService.GetEnrollmentById(enrollmentId);
        return Ok(results);
    }

    [HttpPost("add")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Enrollment>> Enroll([FromBody]Enrollment enrollment)
    {
        await _enrollmentService.Enroll(enrollment);
        return Ok(enrollment+" added successfully");
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteEnrollment([FromBody]Enrollment enrollment)
    {
        await _enrollmentService.RemoveEnrollment(enrollment);
        return Ok("Enrollment deleted successfully");
        
    }
    
}