using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBlogRepository
    {
        Task<Blog> AddBlog(Blog blog);
        Task<List<Blog>> GetAllBlogs();
        Task<Blog?> GetBlogById(Guid blogId);
        Task<Blog> UpdateBlog(Blog blog);
        Task<bool> DeleteBlog(Guid blogId);
        Task<IEnumerable<Blog>> GetBlogsByUserId(Guid userId);
    }
}
