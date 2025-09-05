using authentication.Dtos;
using authentication.Models;

namespace authentication.Services.Interfaces;

public interface IAuthService
{
    public Task<Response<AuthModel>> Register(RegisterModel model);
    public Task<Response<AuthModel>> Login(LoginModel model);
}