using Core.Interfaces.DTO;

namespace Core.Interfaces
{
    /// <summary>
    /// Represents the business logic layer for managing blogs, providing methods for adding, retrieving, updating, and deleting blog entries.
    /// </summary>
    public interface IBlogService
    {
        Task<BlogResponse> AddBlog(AddBlogRequest? blogAddRequest);

        Task<List<BlogResponse>> GetAllBlogs();

        Task<BlogResponse> GetBlogById(Guid? blogId);

        Task<BlogResponse> UpdateBlog(UpdateBlogRequest? updateBlogRequest);

        Task<bool> DeleteBlog(Guid? blogId);
    }
}
