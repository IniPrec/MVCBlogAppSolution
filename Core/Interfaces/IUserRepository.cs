using Core.Domain.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid userId);
    }
}
