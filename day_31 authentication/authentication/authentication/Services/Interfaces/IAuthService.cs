using authentication.Dtos;
using authentication.Models;
using AuthModel = authentication.Models.AuthModel;

namespace authentication.Services.Interfaces;

public interface IAuthService
    {
        Task<Response<AuthModel>> Register(RegisterModel model);
        Task<Response<AuthModel>> Login(LoginModel model);
        Task<Response<string>> SendEmailVerificationOtpAsync(string email);
        Task<Response<bool>> VerifyEmailOtpAsync(string email, string otp);
        Task<Response<string>> SendPasswordResetOtpAsync(string email);
        Task<Response<string>> VerifyPasswordResetOtpAsync(string email, string otp);
        Task<Response<bool>> ChangePasswordAsync(string sessionId, string newPassword);
    }