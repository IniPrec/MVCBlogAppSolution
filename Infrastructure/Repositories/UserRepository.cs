using Dapper;
using Microsoft.Data.SqlClient;
using Core.Domain.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Users (UserId, UserName, Email, PasswordHash, Role) 
                               VALUES (@UserId, @UserName, @Email, @PasswordHash, @Role)";
                await connection.ExecuteAsync(sql, user);

                return user;
            }
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Users WHERE Email = @Email";
                return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
            }
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Users WHERE UserId = @UserId";
                return await connection.QueryFirstOrDefaultAsync<User>(sql, new { UserId = userId });
            }
        }
    }
}
