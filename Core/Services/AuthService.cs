using Core.Interfaces;
using Core.Interfaces.DTO;
using Domain.Entities;
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

            ApplicationUser? existingUsername = await _userRepository.GetUserByUserName(registerRequest.UserName!);

            if (existingUsername != null)
            {
                throw new InvalidOperationException("Username already exist.");
            }

            ApplicationUser? existingUser = await _userRepository.GetUserByEmail(registerRequest.Email!);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email is already registered");
            }

            ApplicationUser user = registerRequest.ToUser();
            ApplicationUser registeredUser = await _userRepository.AddUser(user);

            return registeredUser.ToUserResponse();
        }

        public async Task<UserResponse> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                throw new ArgumentNullException(nameof(loginRequest));
            }

            ApplicationUser? user = await _userRepository.GetUserByEmail(loginRequest.Email!);

            if (user == null)
            {
                throw new InvalidOperationException("Invalid email or password");
            }

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, loginRequest.Password!);

            if (result != PasswordVerificationResult.Success)
            {
                throw new InvalidOperationException("Invalid email or password");
            }

            return user.ToUserResponse();
        }
    }
}
