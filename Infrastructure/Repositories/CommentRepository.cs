using Dapper;
using Microsoft.Data.SqlClient;
using Core.Domain.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string _connectionString;

        public CommentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Comments (CommentId, CommentText, CreatedAt, UpdatedAt, IsDeleted, BlogId, UserId)
                                VALUES (@CommentId, @CommentText, @CreatedAt, @IsDeleted, @BlogId, @UserId)";
                await connection.ExecuteAsync(sql, comment);

                return comment;
            }
        }

        public async Task<List<Comment>> GetCommentsByBlogId(Guid blogId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Comments WHERE BlogId = @BlogId AND IsDeleted = 0";
                var comments = await connection.QueryAsync<Comment>(sql, new { BlogId = blogId });

                return comments.ToList();
            }
        }

        public async Task<Comment?> GetCommentById(Guid commentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Comments WHERE CommentId = @CommentId AND IsDeleted = 0";

                return await connection.QueryFirstOrDefaultAsync<Comment>(sql, new { CommentId = commentId });
            }
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE Comments SET CommentText = @CommentText, WHERE CommentId = @CommentId";
                await connection.ExecuteAsync(sql, comment);

                return comment;
            }
        }

        public async Task<bool> DeleteComment(Guid commentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE Comments SET IsDeleted = 1 WHERE CommentId = @CommentId";
                int rowsAffected = await connection.ExecuteAsync(sql, new { CommentId = commentId });

                return rowsAffected > 0;
            }
        }
    }
}
