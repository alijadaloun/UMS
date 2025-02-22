using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solution1.Application.Handlers.Commands.ClassCommands;
using Solution1.Application.Handlers.Queries.ClassQueries;

namespace Solution1.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClassController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetClassesQuery());
        return Ok(result);
    }

    [HttpGet("class/{classId}")]
    public async Task<IActionResult> GetClassById(int classId)
    {
        var result = await _mediator.Send(new GetClassByIdQuery(classId));
        return Ok(result);
    }

    [HttpPost("class")]
    public async Task<IActionResult> AddClass([FromBody] AddClassCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok($"Class added successfully of id: {result}");
    }

    [HttpDelete("class/{classId}")]
    public async Task<IActionResult> DeleteClass(int classId)
    {
        var result = await _mediator.Send(new DeleteClassCommand(classId));
        return Ok($"Class of id: {classId} deleted successfully");
    }


}