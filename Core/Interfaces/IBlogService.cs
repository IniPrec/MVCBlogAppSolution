using Core.Interfaces.DTO;

namespace Core.Interfaces
{
    /// <summary>
    /// Represents the business logic layer for managing blogs, providing methods for adding, retrieving, updating, and deleting blog entries.
    /// </summary>
    public interface IBlogService
    {
        BlogResponse AddBlog(AddBlogRequest? blogAddRequest);

        List<BlogResponse> GetAllBlogs();

        BlogResponse GetBlogById(Guid? blogId);

        BlogResponse UpdateBlog(UpdateBlogRequest? updateBlogRequest);

        bool DeleteBlog(Guid? blogId);
    }
}
