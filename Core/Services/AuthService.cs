using Core.Interfaces;
using Core.Interfaces.DTO;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Register(RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                throw new ArgumentNullException(nameof(registerRequest));
            }

            User? existingUser = await _userRepository.GetUserByEmail(registerRequest.Email!);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email is already registered");
            }

            User user = registerRequest.ToUser();
            User registeredUser = await _userRepository.AddUser(user);

            return registeredUser.ToUserResponse();
        }

        public async Task<UserResponse> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                throw new ArgumentNullException(nameof(loginRequest));
            }

            User? user = await _userRepository.GetUserByEmail(loginRequest.Email!);

            if (user == null)
            {
                throw new InvalidOperationException("Invalid email or password");
            }

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, loginRequest.Password!);

            if (result != PasswordVerificationResult.Success)
            {
                throw new InvalidOperationException("Invalid email or password");
            }

            return user.ToUserResponse();
        }
    }
}
