using Application.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string _connectionString;

        public RoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Guid> GetRoleIdByName(string roleName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleAsync<Guid>("SELECT RoleId FROM Roles WHERE NormalizedRoleName = @RoleName", new { RoleName = roleName.ToUpper() });
            }
        }
    }
}
