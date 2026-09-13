using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Like
    {
        public Guid LikeId { get; set; }
        public Guid BlogId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
