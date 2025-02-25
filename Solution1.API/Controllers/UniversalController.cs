using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Solution1.Presentation.Resources;

namespace Solution1.Presentation.Controllers;
[ApiController]
[Route("api/universal")]

public class UniversalController: ControllerBase
{
    private readonly IStringLocalizer<Universal> _localizer;

    public UniversalController(IStringLocalizer<Universal> localizer)
    {
        _localizer = localizer;
        
    }

    [HttpGet("greeting")]
    public IActionResult Greeting(string message)
    {
        return Ok(_localizer["WelcomeMessage"]);
    }
    
    
    
}