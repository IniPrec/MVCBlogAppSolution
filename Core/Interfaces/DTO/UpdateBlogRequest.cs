using System;

namespace Core.Interfaces.DTO
{
    public class UpdateBlogRequest
    {
        public Guid BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogContent { get; set; }
    }
}
