using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solution1.Application.Handlers.Commands.UserCommands;

namespace Solution1.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]

public class UserController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UserController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
    {
        _mediator = mediator;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet("signin")]
    public async Task<ActionResult> SignIn(int id, string password)
    {
        var result = await _mediator.Send(new SignInUserCommand(id,password));
        return Ok($"Welcome user of id: {id} you have {result} permissions!");
        
    }

    [HttpPost("profilepic")]
    public async Task<ActionResult> ProfilePicture([FromForm] UploadProfilePictureCommand command)
    {
        var output = await _mediator.Send(command);
        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
        string uniquepath = Guid.NewGuid().ToString() + Path.GetExtension(command.file.FileName);
        string filePath = Path.Combine(uploadDir, uniquepath);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        { 
           await command.file.CopyToAsync(stream);
        }
        return Ok(output?"uploaded profile picture":"failed to upload profile picture");
    }

    [HttpPost("signup")]
    public async Task<ActionResult> SignUp([FromBody]SignUpUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok($"Welcome user: {command.userName} you have {result} permissions!");
        
    }
}