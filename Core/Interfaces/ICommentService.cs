using Core.Interfaces.DTO;

namespace Core.Interfaces
{
    public interface ICommentService
    {
        Task<CommentResponse> AddComment(AddCommentRequest addCommentRequest);
        Task<List<CommentResponse>> GetCommentsByBlogId(Guid blogId);
        Task<CommentResponse> GetCommentById(Guid blogId);
        Task<CommentResponse> UpdateComment(UpdateCommentRequest updateCommentRequest);
        Task<bool> DeleteComment(Guid commentId);
    }
}
