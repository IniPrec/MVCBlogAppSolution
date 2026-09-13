using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts.DTO
{
    public class AddCommentRequest
    {
        public Guid BlogId { get; set; }
        public Guid UserId {  get; set; }
        public string? CommentText { get; set; }

        public Comment ToComment()
        {
            return new Comment
            {
                CommentId = Guid.NewGuid(),
                BlogId = this.BlogId,
                UserId = this.UserId,
                CommentText = this.CommentText,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
