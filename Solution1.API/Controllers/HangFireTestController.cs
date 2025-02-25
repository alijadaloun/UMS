using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solution1.Application.Handlers.Commands;
using Solution1.Application.Handlers.Commands.HangFireCommands;

namespace Solution1.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class HangFireTestController:ControllerBase
{
    private readonly IMediator _mediator;

    public HangFireTestController(IMediator mediator)
    {
        _mediator = mediator;

    }
    [HttpGet("grade")]
    public async Task<IActionResult> GradeInBackground(int studentId, int grade)
    {
        var email = await _mediator.Send(new HangFireNotifyCommand(studentId, grade));
        return Ok($"sent an email to: {email} that his new grade is: {grade}");

    }
    [HttpGet("notify")]
    public async Task<IActionResult> NotifyDaily(int studentId, string enrollmentDeadline)
    {
        var email = _mediator.Send(new NotifyDailyDeadlineCommand(studentId, enrollmentDeadline));
        
        return Ok($"sent an email to: {await email} that his enrollment deadline is: {enrollmentDeadline}");
    }


}