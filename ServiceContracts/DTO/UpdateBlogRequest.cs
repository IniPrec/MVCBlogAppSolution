using System;

namespace ServiceContracts.DTO
{
    public class UpdateBlogRequest
    {
        public Guid BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogContent { get; set; }
    }
}
