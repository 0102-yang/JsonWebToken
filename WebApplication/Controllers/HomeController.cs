using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JsonWebToken.Web.Controllers;

[Route("api")]
[ApiController]
public class HomeController(ILogger<HomeController> logger) : ControllerBase
{
    [HttpGet("home")]
    public IActionResult Home()
    {
        logger.LogInformation("Home!");
        return this.Ok("Hello world!");
    }

    [HttpGet("classify")]
    [Authorize(Roles = "Admin")]
    public IActionResult Classify()
    {
        logger.LogInformation("Classify!");
        return this.Ok("You are classified!");
    }
}
