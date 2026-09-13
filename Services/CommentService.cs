using ServiceContracts;
using ServiceContracts.DTO;
using Entities;

namespace Services
{
    public class CommentService : ICommentService
    {
        private readonly List<Comment> _comments;

        public CommentService()
        {
            _comments = new List<Comment>();
        }

        public CommentResponse AddComment(AddCommentRequest? addCommentRequest)
        {
            if (addCommentRequest == null)
            {
                throw new ArgumentNullException(nameof(addCommentRequest));
            }

            if (string.IsNullOrEmpty(addCommentRequest.CommentText))
            {
                throw new ArgumentException("Comment text cannot be null or empty", nameof(addCommentRequest.CommentText));
            }

            Comment comment = addCommentRequest.ToComment();
            _comments.Add(comment);

            return comment.ToCommentResponse();
        }

        public bool DeleteComment(Guid? commentId)
        {
            if (commentId == null)
            {
                throw new ArgumentNullException(nameof(commentId), "Comment ID cannot be null");
            }

            Comment? comment = _comments.FirstOrDefault(temp => temp.CommentId == commentId);

            if (comment == null)
            {
                return false;
            }

            _comments.Remove(comment);
            return true;
        }

        public List<CommentResponse> GetAllComments()
        {
            return _comments.Select(comment => comment.ToCommentResponse()).ToList();
        }

        public CommentResponse GetCommentById(Guid? commentId)
        {
            if (commentId == null)
            {
                return null;
            }

            Comment? comment = _comments.FirstOrDefault(temp =>  temp.CommentId == commentId);

            if (comment == null)
            {
                return null;
            }

            return comment.ToCommentResponse();
        }

        public CommentResponse UpdateComment(UpdateCommentRequest? updateCommentRequest)
        {
            if (updateCommentRequest == null)
            {
                throw new ArgumentNullException(nameof(updateCommentRequest), "Update request cannot be null");
            }

            Comment? existingcomment = _comments.FirstOrDefault(temp => temp.CommentId == updateCommentRequest.CommentId);
            
            if (existingcomment == null)
            {
                throw new ArgumentException("Comment with given ID doesn't exist");
            }

            existingcomment.CommentText = updateCommentRequest.CommentText;

            return existingcomment.ToCommentResponse();
        }
    }
}
