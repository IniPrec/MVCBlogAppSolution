using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.DTO
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }

    public static class UserResponseExtensions
    {
        public static UserResponse ToUserResponse(this ApplicationUser user)
        {
            return new UserResponse
            {

                UserId = Guid.Parse(user.Id),
                UserName = user.UserName,
                Email = user.Email,
                Role = UserManager<ApplicationUser>.GetRolesAsync(user),
            };
        }
    }
}
