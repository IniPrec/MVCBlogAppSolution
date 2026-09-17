using Core.Interfaces;
using Core.Interfaces.DTO;
using Core.Domain.Entities;

namespace Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentResponse> AddComment(AddCommentRequest? addCommentRequest)
        {
            if (addCommentRequest == null)
            {
                throw new ArgumentNullException(nameof(addCommentRequest));
            }

            if (string.IsNullOrEmpty(addCommentRequest.CommentText))
            {
                throw new ArgumentException("Comment text cannot be null or empty");
            }

            Comment comment = addCommentRequest.ToComment();
            Comment addedComment = await _commentRepository.AddComment(comment);

            return addedComment.ToCommentResponse();
        }

        public async Task<List<CommentResponse>> GetCommentsByBlogId(Guid blogId)
        {
            List<Comment> comments = await _commentRepository.GetCommentsByBlogId(blogId);

            return comments.Select(c => c.ToCommentResponse()).ToList();
        }

        public async Task<CommentResponse?> GetCommentById(Guid commentId)
        {
            if (commentId == null) return null;
            Comment? comment = await _commentRepository.GetCommentById(commentId);

            return comment?.ToCommentResponse();
        }

        public async Task<CommentResponse> UpdateComment(UpdateCommentRequest? updateCommentRequest)
        {
            if (updateCommentRequest == null)
            {
                throw new ArgumentNullException(nameof(updateCommentRequest));
            }

            Comment? existingComment = await _commentRepository.GetCommentById(updateCommentRequest.CommentId);

            if (existingComment == null)
            {
                throw new ArgumentException("Comment does not exist");
            }

            existingComment.CommentText = updateCommentRequest.CommentText;
            Comment updated = await _commentRepository.UpdateComment(existingComment);

            return updated.ToCommentResponse();
        }

        public async Task<bool> DeleteComment(Guid commentId)
        {
            if (commentId == null)
            {
                throw new ArgumentNullException(nameof(commentId));
            }

            return await _commentRepository.DeleteComment(commentId);
        }
    }
}
