using Microsoft.AspNetCore.Mvc;

namespace cleanArchitecture.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class testController: ControllerBase
{
    [HttpGet("test")]
    public IActionResult test()
    {
        return Ok("test");
    }
}