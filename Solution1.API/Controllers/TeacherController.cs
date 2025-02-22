using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solution1.Application.Handlers.Commands.TeacherCommands;
using Solution1.Application.Handlers.Queries.TeacherQueries;

namespace Solution1.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TeacherController: ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetTeachers()
    {
        return Ok(await _mediator.Send(new GetTeachersQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeacherById(int id)
    {
        var teacher = await _mediator.Send(new GetTeacherByIdQuery(id));
        return Ok(teacher);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddTeacher([FromBody] AddTeacherCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok($"Teacher added successfully of id: {result}");
        
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        var result = await _mediator.Send(new DeleteTeacherCommand(id));
        return Ok($"Teacher of id: {id} deleted successfully");
    }
    
}