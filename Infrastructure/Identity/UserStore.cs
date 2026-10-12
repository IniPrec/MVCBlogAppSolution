using Dapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Identity
{
    public class UserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IUserRoleStore<ApplicationUser>
    {
        private readonly string? _connectionString;

        public UserStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BlogServerDb");
        }


        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Users (UserId, UserName, Email, PasswordHash, RoleId) 
                               VALUES (@UserId, @UserName, @Email, @PasswordHash, @RoleId)";
                await connection.ExecuteAsync(sql, new
                {
                    UserId = user.Id,
                    user.UserName,
                    user.Email,
                    user.PasswordHash,
                    user.RoleId
                });

                return IdentityResult.Success;
            }
        }
        public async Task<ApplicationUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT UserId AS Id, UserName, Email, PasswordHash, RoleId FROM Users WHERE Email = @Email";
                return await connection.QueryFirstOrDefaultAsync<ApplicationUser>(sql, new { Email = normalizedEmail });
            }
        }
        public async Task<ApplicationUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT UserId AS Id, UserName, Email, PasswordHash, RoleId FROM Users WHERE UserId = @UserId";
                return await connection.QueryFirstOrDefaultAsync<ApplicationUser>(sql, new { UserId = Guid.Parse(userId) });
            }
        }
        public async Task<ApplicationUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT UserId AS Id, UserName, Email, PasswordHash, RoleId FROM Users WHERE UserName = @UserName";
                return await connection.QueryFirstOrDefaultAsync<ApplicationUser>(sql, new { UserName = normalizedUserName });
            }
        }
        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var roleId = await connection.QuerySingleAsync<Guid>("SELECT RoleId FROM Roled WHERE NormalisedName = @Name", new { Name = roleName.ToUpper() });

                await connection.ExecuteAsync("UPDATE Users SET RoleId = @RoleId WHERE UserId = @UserId", new { RoleId = roleId, UserId = user.Id });

                user.RoleId = roleId;
            }
        }
        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var userRoleId = await connection.QuerySingleAsync<Guid>("SELECT RoleId FROM Roles WHERE NormalisedName = USER");

                await connection.ExecuteAsync("UPDATE Users SET RoleId = @RoleId WHERE UserId = @UserId");

                user.RoleId = userRoleId;
            }
        }
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var roleName = await connection.QuerySingleOrDefaultAsync<string>("SELECT Name FROM Roles WHERE RoleId = @RoleId", new { user.RoleId });

                return roleName == null ? new List<string>() : new List<string> { roleName };
            }
        }
        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var count = await connection.QuerySingleAsync<int>(@"SELECT COUNT(*) FROM Users u JOIN Roles r ON r.RoleId = u.RoleId WHERE u.UserId = @UserId AND r.NormalisedName = @Name", new { UserId = user.Id, Name = roleName.ToUpper() });

                return count > 0;
            }
        }
        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var users = await connection.QueryAsync<ApplicationUser>(@"SELECT u.UserId AS Id, u.UserName, u.Email, u.PasswordHash, u.RoleId FROM Users u JOIN Roles r ON r.RoleId = u.RoleId WHERE r.NormalisedName = @Name", new { Name = roleName.ToUpper() });

                return users.ToList();
            }
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<string?> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }



        public Task SetEmailAsync(ApplicationUser user, string? email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string? normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string? passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(ApplicationUser user, string? userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
