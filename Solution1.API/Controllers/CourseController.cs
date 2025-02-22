using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solution1.Application.Handlers.Commands.CourseCommands;
using Solution1.Application.Handlers.Queries.CourseQueries;

namespace Solution1.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetCoursesQuery());
        return Ok(result);

    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetCourseById(int courseId)
    {
        var result = await _mediator.Send(new GetCourseByIdQuery(courseId));
        return Ok(result);
    }

    [HttpPost("course")]
    public async Task<IActionResult> AddCourse(AddCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok($"Course added successfully of id: {result}");
    }

    [HttpDelete("course/{courseId}")]
    public async Task<IActionResult> DeleteCourse(int courseId)
    {
        var result = await _mediator.Send(new DeleteCourseCommand(courseId));
        return Ok($"Course of id: {courseId} has been deleted");
    }
}