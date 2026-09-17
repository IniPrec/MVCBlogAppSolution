using Core.Domain.Entities;
using Core.Interfaces.DTO;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly string _connectionString;

        public LikeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Like> AddLike(Like like)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Likes (LikeId, BlogId, UserId, CreatedAt)
                    VALUES (@LikeId, @BlogId, @UserId, @CreatedAt);";
                await connection.ExecuteAsync(sql, like);

                return like;
            }
        }

        public async Task<Like?> GetLike(Guid blogId, Guid userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Likes WHERE BlogId = @BlogId AND UserId = @UserId";

                return await connection.QueryFirstOrDefaultAsync<Like>(sql, new { BlogId = blogId, UserId = userId });
            }
        }

        public async Task<int> GetLikeCountByBlogId(Guid blogId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT COUNT(*) FROM Likes WHERE BlogId = @BlogId";

                return await connection.ExecuteScalarAsync<int>(sql, new { BlogId = blogId });
            }
        }

        public async Task<bool> RemoveLike(Guid blogId, Guid userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Likes WHERE BlogId = @BlogId AND UserId = @UserId";
                int rows = await connection.ExecuteAsync(sql, new { BlogId = blogId, UserId = userId });

                return rows > 0;    
            }
        }
    }
}
