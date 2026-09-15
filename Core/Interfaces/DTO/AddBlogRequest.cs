using System;
using Core.Domain.Entities;

namespace Core.Interfaces.DTO
{
    /// <summary>
    /// DTO class for adding a new blog
    /// </summary>
    public class AddBlogRequest
    {
        public Guid UserId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogContent { get; set; }

        public Blog ToBlog()
        {
            return new Blog
            {
                BlogId = Guid.NewGuid(),
                BlogTitle = this.BlogTitle,
                BlogContent = this.BlogContent,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                UserId = this.UserId
            };
        }
    }
}
