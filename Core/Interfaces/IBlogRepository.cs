using Core.Domain.Entities;

namespace Core.Interfaces
{
    public interface IBlogRepository
    {
        Task<Blog> AddBlog(Blog blog);
        Task<List<Blog>> GetAllBlogs();
        Task<Blog?> GetBlogById(Guid blogId);
        Task<Blog> UpdateBlog(Blog blog);
        Task<bool> DeleteBlog(Guid blogId);
    }
}
