using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Comment
    {
        public Guid CommentId {  get; set; }
        public Guid BlogId { get; set; }
        public Guid UserId { get; set; }
        public string? CommentText { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
