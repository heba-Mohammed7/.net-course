using AutoMapper;
using authentication.Data;
using authentication.Dtos;
using authentication.Helpers;
using authentication.Models;
using authentication.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using authentication.Settings;
using AuthModel = authentication.Models.AuthModel;

namespace authentication.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly DataProtectionTokenProviderSetting _tokenSettings;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IOptions<JWT> jwt,
            ApplicationDbContext context,
            IEmailService emailService,
            IOptions<DataProtectionTokenProviderSetting> tokenSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwt = jwt.Value;
            _context = context;
            _emailService = emailService;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<Response<AuthModel>> Register(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return new Response<AuthModel>
                {
                    Message = "Email already exists",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                return new Response<AuthModel>
                {
                    Message = $"Role {model.Role} does not exist",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var user = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(x => x.Description));
                return new Response<AuthModel>
                {
                    Message = errors,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            await _userManager.AddToRoleAsync(user, model.Role);

            var otpResponse = await SendEmailVerificationOtpAsync(model.Email);
            if (!otpResponse.Status)
            {
                await _userManager.DeleteAsync(user);
                return new Response<AuthModel>
                {
                    Message = otpResponse.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return new Response<AuthModel>
            {
                Message = "User registered successfully. Please verify your email with the OTP sent.",
                StatusCode = HttpStatusCode.OK,
                Data = new AuthModel
                {
                    UserName = user.UserName,
                    Role = model.Role
                }
            };
        }

        public async Task<Response<AuthModel>> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return new Response<AuthModel>
                {
                    Message = "Invalid credentials",
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            if (!user.IsEmailVerified)
            {
                return new Response<AuthModel>
                {
                    Message = "Email not verified. Please verify your email first.",
                    StatusCode = HttpStatusCode.Forbidden
                };
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return new Response<AuthModel>
                {
                    Message = "Invalid credentials",
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            return new Response<AuthModel>
            {
                StatusCode = HttpStatusCode.OK,
                Data = new AuthModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
                    UserName = user.UserName
                },
                Message = "Login successful"
            };
        }

        public async Task<Response<string>> SendEmailVerificationOtpAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response<string>
                {
                    Message = "User not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            if (user.IsEmailVerified)
            {
                return new Response<string>
                {
                    Message = "Email is already verified",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var otp = GenerateOtp();
            var token = Guid.NewGuid().ToString();
            var expiresAt = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpiresIn);

            var otpSession = new OtpSession
            {
                UserId = user.Id,
                OtpCode = otp,
                SessionId = token,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt,
                IsUsed = false,
                Purpose = "EmailVerification"
            };

            _context.OtpSessions.Add(otpSession);
            await _context.SaveChangesAsync();

            var emailModel = new ConfirmEmailModel(
                ToName: $"{user.FirstName} {user.LastName}",
                ToMail: user.Email,
                Code: otp,
                Token: token,
                ExpiredInMinutes: _tokenSettings.ExpiresIn
            );

            try
            {
                await _emailService.SendEmailAsync(emailModel, EmailSubject.EmailVerification, HtmlTemplate.ConfirmEmail);
                return new Response<string>
                {
                    Status = true,
                    Data = token,
                    Message = "Verification OTP sent successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Message = $"Failed to send OTP: {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<bool>> VerifyEmailOtpAsync(string email, string otp)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response<bool>
                {
                    Message = "User not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var otpSession = await _context.OtpSessions
                .FirstOrDefaultAsync(os => os.UserId == user.Id &&
                                          os.OtpCode == otp &&
                                          os.Purpose == "EmailVerification" &&
                                          !os.IsUsed &&
                                          os.ExpiresAt > DateTime.UtcNow);

            if (otpSession == null)
            {
                return new Response<bool>
                {
                    Message = "Invalid or expired OTP",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            user.IsEmailVerified = true;
            otpSession.IsUsed = true;
            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();

            return new Response<bool>
            {
                Status = true,
                Data = true,
                Message = "Email verified successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
public async Task<Response<string>> SendPasswordResetOtpAsync(string email)
{
    var user = await _userManager.FindByEmailAsync(email);
    if (user == null)
    {
        return new Response<string>
        {
            Message = "User not found",
            StatusCode = HttpStatusCode.NotFound
        };
    }

    var otp = GenerateOtp();
    var sessionId = Guid.NewGuid().ToString();
    var expiresAt = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpiresIn);

    var otpSession = new OtpSession
    {
        UserId = user.Id,
        OtpCode = otp,
        SessionId = sessionId,
        CreatedAt = DateTime.UtcNow,
        ExpiresAt = expiresAt,
        IsUsed = false,
        Purpose = "PasswordReset"
    };

    _context.OtpSessions.Add(otpSession);
    await _context.SaveChangesAsync();

    var emailModel = new ResetPasswordModel(
        ToName: $"{user.FirstName} {user.LastName}",
        ToMail: user.Email,
        Token: sessionId,
        Code: otp,
        ExpiredInMinutes: _tokenSettings.ExpiresIn
    )
    {
        SessionId = sessionId,
    };

    try
    {
        await _emailService.SendEmailAsync(emailModel, EmailSubject.PasswordReset, HtmlTemplate.ResetPassword);
        return new Response<string>
        {
            Status = true,
            Data = sessionId,
            Message = "Password reset OTP sent successfully",
            StatusCode = HttpStatusCode.OK
        };
    }
    catch (Exception ex)
    {
        return new Response<string>
        {
            Message = $"Failed to send OTP: {ex.Message}",
            StatusCode = HttpStatusCode.InternalServerError
        };
    }
}
        public async Task<Response<string>> VerifyPasswordResetOtpAsync(string email, string otp)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response<string>
                {
                    Message = "User not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var otpSession = await _context.OtpSessions
                .FirstOrDefaultAsync(os => os.UserId == user.Id &&
                                          os.OtpCode == otp &&
                                          os.Purpose == "PasswordReset" &&
                                          !os.IsUsed &&
                                          os.ExpiresAt > DateTime.UtcNow);

            if (otpSession == null)
            {
                return new Response<string>
                {
                    Message = "Invalid or expired OTP",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            otpSession.IsUsed = true;
            await _context.SaveChangesAsync();

            return new Response<string>
            {
                Status = true,
                Data = otpSession.SessionId,
                Message = "OTP verified successfully. Use the session ID to reset your password.",
                StatusCode = HttpStatusCode.OK
            };
        }

public async Task<Response<bool>> ChangePasswordAsync(string sessionId, string newPassword)
{
    var otpSession = await _context.OtpSessions
        .FirstOrDefaultAsync(os => os.SessionId == sessionId &&
                                  os.Purpose == "PasswordReset" &&
                                  os.IsUsed &&
                                  os.ExpiresAt > DateTime.UtcNow);

    if (otpSession == null)
    {
        return new Response<bool>
        {
            Message = "Invalid or expired session ID",
            StatusCode = HttpStatusCode.BadRequest
        };
    }

    var user = await _userManager.FindByIdAsync(otpSession.UserId);
    if (user == null)
    {
        return new Response<bool>
        {
            Message = "User not found",
            StatusCode = HttpStatusCode.NotFound
        };
    }

    try
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(x => x.Description));
            return new Response<bool>
            {
                Message = errors,
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        var userOtpSessions = await _context.OtpSessions
            .Where(os => os.UserId == user.Id && os.Purpose == "PasswordReset")
            .ToListAsync();
        _context.OtpSessions.RemoveRange(userOtpSessions);
        await _context.SaveChangesAsync();

        return new Response<bool>
        {
            Status = true,
            Data = true,
            Message = "Password changed successfully",
            StatusCode = HttpStatusCode.OK
        };
    }
    catch (Exception ex)
    {
        return new Response<bool>
        {
            Message = $"Password reset failed: {ex.Message}",
            StatusCode = HttpStatusCode.InternalServerError
        };
    }
}
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault())
            }
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.ExpireDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private static string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}