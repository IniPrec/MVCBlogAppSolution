using Application.Interfaces;
using Application.Interfaces.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<ApplicationUser> _userManager;

		public CommentService(ICommentRepository commentRepository, UserManager<ApplicationUser> userManager)
        {
            _commentRepository = commentRepository;
			_userManager = userManager;
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
            List<CommentResponse> responses = new List<CommentResponse>();

            foreach (var comment in comments)
            {
                CommentResponse response = comment.ToCommentResponse();
                ApplicationUser? user = await _userManager.FindByIdAsync(comment.UserId.ToString());
				response.UserName = user?.UserName ?? "Unknown";
                responses.Add(response);
            }

            return responses;
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
