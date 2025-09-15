using Microsoft.AspNetCore.Mvc;

namespace cleanArchitecture.Preentation2.Controllers;
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