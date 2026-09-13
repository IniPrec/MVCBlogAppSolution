using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts.DTO
{
    public class UpdateCommentRequest
    {
        public Guid CommentId { get; set; }
        public string? CommentText { get; set; }
    }
}
