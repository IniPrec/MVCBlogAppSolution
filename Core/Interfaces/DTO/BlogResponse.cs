using System;
using Core.Domain.Entities;

namespace Core.Interfaces.DTO
{
    public class BlogResponse
    {
        public Guid BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }

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

    public static class BlogResponseExtensions
    {
        public static BlogResponse ToBlogResponse(this Blog blog)
        {
            return new BlogResponse()
            {
                BlogId = blog.BlogId,
                BlogTitle = blog.BlogTitle,
                BlogContent = blog.BlogContent,
                CreatedAt = blog.CreatedAt,
                UserName = blog.UserName,
            };
        }
    }
}
