using Domain.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> AddUser(ApplicationUser user);
        Task<ApplicationUser?> GetUserByUserName(string userName);
        Task<ApplicationUser?> GetUserByEmail(string email);
        Task<ApplicationUser?> GetUserById(Guid userId);
    }
}
