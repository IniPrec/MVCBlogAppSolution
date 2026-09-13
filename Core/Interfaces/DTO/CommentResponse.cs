using System;
using Core.Domain.Entities;

namespace Core.Interfaces.DTO
{
    public class CommentResponse
    {
        public Guid CommentId { get; set; }
        public Guid BlogId { get; set; }
        public Guid UserId { get; set; }
        public string? CommentText { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public static class CommentResponseExtensions
    {
        public static CommentResponse ToCommentResponse(this Comment comment)
        {
            return new CommentResponse
            {
                CommentId = comment.CommentId,
                BlogId = comment.BlogId,
                UserId = comment.UserId,
                CommentText = comment.CommentText,
                CreatedAt = comment.CreatedAt
            };
        }
    }
}
