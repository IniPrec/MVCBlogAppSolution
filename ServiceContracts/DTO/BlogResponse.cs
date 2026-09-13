using System;
using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class used as a return type for the AddBlog operation in the IBlogService interface
    /// </summary>
    public class BlogResponse
    {
        public Guid BlogId { get; set; }
        public string? BlogTitle { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(BlogResponse))
            {
                return false;
            }
            BlogResponse compareBlog = (BlogResponse) obj;

            return this.BlogId == compareBlog.BlogId && BlogTitle == compareBlog.BlogTitle;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class AddBlogResponseExtensions
    {
        public static BlogResponse ToBlogResponse(this Blog blog)
        {
            return new BlogResponse()
            {
                BlogId = blog.BlogId,
                BlogTitle = blog.BlogTitle
            };
        }
    }
}
