
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solution1.Application.Handlers.Commands.TeacherCommands;
using Solution1.Application.Handlers.Commands.UserCommands;
using Solution1.Application.Handlers.Queries.TeacherQueries;

namespace Solution1.Presentation.Controllers;
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TeacherController: ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeachers()
    {
        return Ok(await _mediator.Send(new GetTeachersQuery()));
    }

    [HttpGet("{id}")]
    [ApiVersion("1.0")]
    public async Task<IActionResult> GetTeacherById(int id)
    {
        var teacher = await _mediator.Send(new GetTeacherByIdQuery(id));
        return Ok(teacher.TeacherName +" Version 1.0");
    }
    [HttpGet("{id}")]
    [ApiVersion("2.0")]
    public async Task<IActionResult> GetTeacherByIdV2(int id)
    {
        var teacher = await _mediator.Send(new GetTeacherByIdQuery(id));
        return Ok(teacher.TeacherName +" Version 2.0");
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
    [HttpPost("grade")]
    public async Task<IActionResult> Grade(int studentid, int grade,int teacherid, string password)//out of 20
    {
        var r = await _mediator.Send(new SignInUserCommand( teacherid, password));
        if (r.Equals("Admin"))
        {
            var a = await _mediator.Send(new GradeStudentCommand( studentid,grade));
            
            return Ok($"Student of id: {studentid} has been graded");
            
            
        }
        else
        {
            return BadRequest("Wrong Password");
        }
        
        
        
    }

    
}