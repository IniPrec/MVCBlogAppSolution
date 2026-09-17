using Core.Domain.Entities;

namespace Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> AddComment(Comment comment);
        Task<List<Comment>> GetCommentsByBlogId(Guid blogId);
        Task<Comment?> GetCommentById(Guid commentId);
        Task<Comment> UpdateComment(Comment comment);
        Task<bool> DeleteComment(Guid commentId);
    }
}
