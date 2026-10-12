using Application.Interfaces.DTO;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(RegisterRequest register);
        Task<SignInResult> Login(LoginRequest login);
        Task Logout();
        Task<bool> UserNameExists(string userName);
        Task<bool> EmailExists(string email);
    }
}
