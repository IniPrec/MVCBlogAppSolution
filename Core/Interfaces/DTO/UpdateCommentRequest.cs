using Core.Domain.Entities;

namespace Core.Interfaces.DTO
{
    public class UpdateCommentRequest
    {
        public Guid CommentId { get; set; }
        public string? CommentText { get; set; }
    }
}
