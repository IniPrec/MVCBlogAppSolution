using Core.Interfaces.DTO;

namespace Core.Interfaces
{
    public interface ICommentService
    {
        CommentResponse AddComment(AddCommentRequest? addCommentRequest);

        List<CommentResponse> GetAllComments();

        CommentResponse GetCommentById(Guid? commentId);

        CommentResponse UpdateComment(UpdateCommentRequest? updateCommentRequest);

        bool DeleteComment(Guid? commentId);
    }
}
