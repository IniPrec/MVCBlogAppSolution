using Dapper;
using Microsoft.Data.SqlClient;
using Core.Domain.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly string _connectionString;

        public BlogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Blog> AddBlog(Blog blog)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Blogs (BlogId, BlogTitle, BlogContent, CreatedAt, UpdatedAt, IsDeleted, UserId)
                                VALUES (@BlogId, @BlogTitle, @BlogContent, @CreatedAt, @UpdatedAt, @IsDeleted, @UserId)";
                await connection.ExecuteAsync(sql, blog);
                return blog;
            }
        }

        public async Task<List<Blog>> GetAllBlogs()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Blogs WHERE IsDeleted = 0";
                var blogs = await connection.QueryAsync<Blog>(sql);
                return blogs.ToList();
            }
        }

        public async Task<Blog?> GetBlogById(Guid blogId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Blogs WHERE BlogId = @BlogId AND IsDeleted = 0";
                return await connection.QueryFirstOrDefaultAsync<Blog>(sql, new { BlogId = blogId });
            }
        }

        public async Task<Blog> UpdateBlog(Blog blog)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE Blogs SET BlogTitle = @BlogTitle, BlogContent = @BlogContent, UpdatedAt = @UpdatedAt
                                WHERE BlogId = @BlogId";
                await connection.ExecuteAsync(sql, blog);
                return blog;
            }
        }

        public async Task<bool> DeleteBlog(Guid blogId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Blogs SET IsDeleted = 1 WHERE BlogId = @BlogId";
                int rowsAffected = await connection.ExecuteAsync(sql, new { BlogId = blogId });
                return rowsAffected > 0;
            }
        }
    }
}
