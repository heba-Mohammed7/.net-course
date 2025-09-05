
using System.Net;
using authentication.Controllers.Base;
using authentication.Dtos;
using authentication.Models;
using authentication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authentication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService _authService) : BaseController
{
    

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return Result(new Response<object>
            {
                Data = ModelState,
                StatusCode = HttpStatusCode.BadRequest
            });
        }
        var result = await _authService.Register(model);
        return Result(result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return Result(new Response<object>
            {
                Data = ModelState,
                StatusCode = HttpStatusCode.BadRequest
            });
        }
        var result = await _authService.Login(model);
        return Result(result);
    }

    
}