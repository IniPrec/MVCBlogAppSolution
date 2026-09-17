using Core.Interfaces.DTO;

namespace Core.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponse> Register(RegisterRequest registerRequest);
        Task<UserResponse> Login(LoginRequest loginRequest);
    }
}
