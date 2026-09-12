using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents the business logic layer for managing blogs, providing methods for adding, retrieving, updating, and deleting blog entries.
    /// </summary>
    public interface IBlogService
    {
        /// <summary>
        /// Adds a new blog entry based on the provided AddBlogRequest and returns an AddBlogResponse containing the details of the newly created blog.
        /// </summary>
        /// <param name="blogAddRequest">Blog object to add</param>
        /// <returns>returns the blog object after adding it (including newly generated blog id)</returns>
        AddBlogResponse AddBlog(AddBlogRequest? blogAddRequest);
    }
}
