using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solution1.Application.Handlers.Commands;
using Solution1.Application.Handlers.Commands.StudentCommands;
using Solution1.Application.Handlers.Queries;
using Solution1.Application.Handlers.Queries.StudentQueries;
using Solution1.Domain.Entities;

namespace Solution1.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentController: ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetStudentsQuery()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudent([FromRoute]int id)
    {
      var result = await _mediator.Send(new GetStudentByIdQuery(id));
      if( result == null ) return StatusCode(404,"No students yet");
      return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCourseCommand command)
    {
        var student = await _mediator.Send(command);
        return Ok($"student of id: {student.Id} registered.");
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok($"Student added succesfully with id: {result}");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteStudent( int id)
    {
        var result = await _mediator.Send(new DeleteStudentCommand(id));
        
        return Ok($"Student of id: {id} has been deleted");
    }
    
    
}