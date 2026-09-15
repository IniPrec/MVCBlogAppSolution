using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces.DTO
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public string Role { get; set; } = "Viewer";

        public User ToUser()
        {
            User user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = this.UserName,
                Email = this.Email,
                Role = this.Role
            };

            PasswordHasher<User> hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, this.Password!);

            return user;
        }
    }
}
