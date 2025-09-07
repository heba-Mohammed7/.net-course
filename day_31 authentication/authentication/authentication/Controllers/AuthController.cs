
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

    [HttpPost]
    [Route("send-email-verification")]
    public async Task<IActionResult> SendEmailVerificationOtp([FromBody] EmailRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return Result(new Response<object>
            {
                Data = ModelState,
                StatusCode = HttpStatusCode.BadRequest
            });
        }
        var result = await _authService.SendEmailVerificationOtpAsync(model.Email);
        return Result(result);
    }
    [HttpPost]
    [Route("verify-email")]
    public async Task<IActionResult> VerifyEmailOtp([FromBody] VerifyOtpModel model)
    {
        if (!ModelState.IsValid)
        {
            return Result(new Response<object>
            {
                Data = ModelState,
                StatusCode = HttpStatusCode.BadRequest
            });
        }
        var result = await _authService.VerifyEmailOtpAsync(model.Email, model.Otp);
        return Result(result);
    }
    [HttpPost]
    [Route("send-password-reset")]
    public async Task<IActionResult> SendPasswordResetOtp([FromBody] EmailRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return Result(new Response<object>
            {
                Data = ModelState,
                StatusCode = HttpStatusCode.BadRequest
            });
        }
        var result = await _authService.SendPasswordResetOtpAsync(model.Email);
        return Result(result);
    }
    [HttpPost]
    [Route("verify-password-reset")]
    public async Task<IActionResult> VerifyPasswordResetOtp([FromBody] VerifyOtpModel model)
    {
        if (!ModelState.IsValid)
        {
            return Result(new Response<object>
            {
                Data = ModelState,
                StatusCode = HttpStatusCode.BadRequest
            });
        }
        var result = await _authService.VerifyPasswordResetOtpAsync(model.Email, model.Otp);
        return Result(result);
    }
    [HttpPost]
    [Route("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            return Result(new Response<object>
            {
                Data = ModelState,
                StatusCode = HttpStatusCode.BadRequest
            });
        }
        var result = await _authService.ChangePasswordAsync(model.SessionId, model.NewPassword);
        return Result(result);
    }
}