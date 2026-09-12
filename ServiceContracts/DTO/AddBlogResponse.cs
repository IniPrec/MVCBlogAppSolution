using System;
using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class used as a return type for the AddBlog operation in the IBlogService interface
    /// </summary>
    public class AddBlogResponse
    {
        public Guid BlogId { get; set; }
        public string? BlogTitle { get; set; }
    }

    public static class AddBlogResponseExtensions
    {
        public static AddBlogResponse ToAddBlogResponse(this Blog blog)
        {
            return new AddBlogResponse()
            {
                BlogId = blog.BlogId,
                BlogTitle = blog.BlogTitle
            };
        }
    }
}
