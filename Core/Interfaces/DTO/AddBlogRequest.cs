using System;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Core.Interfaces.DTO
{
    /// <summary>
    /// DTO class for adding a new blog
    /// </summary>
    public class AddBlogRequest
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Title cannot be blank")]
        public string? BlogTitle { get; set; }

        [Required(ErrorMessage = "Content cannot be blank")]
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
