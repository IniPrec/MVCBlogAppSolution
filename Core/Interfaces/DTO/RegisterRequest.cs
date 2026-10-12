using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.DTO
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Compare("Email", ErrorMessage = "Email already exist")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirmation is required")]
        [Compare("Password", ErrorMessage = "Password must match")]
        public string? ConfirmPassword { get; set; }

        public string Role { get; set; } = "User";
    }
}
