using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts
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
