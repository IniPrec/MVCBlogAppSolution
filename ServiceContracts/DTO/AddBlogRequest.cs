using System;
using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class for adding a new blog
    /// </summary>
    public class AddBlogRequest
    {
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
                IsDeleted = false
            };
        }
    }
}
