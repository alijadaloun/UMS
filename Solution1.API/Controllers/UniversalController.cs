using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Solution1.Presentation.Controllers;
[ApiController]
[Route("api/universal")]

public class UniversalController: ControllerBase
{
    private readonly IStringLocalizer<UniversalController> _localizer;

    public UniversalController(IStringLocalizer<UniversalController> localizer)
    {
        _localizer = localizer;
        
    }

    [HttpGet("greeting")]
    public IActionResult Greeting(string message)
    {
        return Ok(_localizer["WelcomeMessage"]);
    }
    
    
    
}