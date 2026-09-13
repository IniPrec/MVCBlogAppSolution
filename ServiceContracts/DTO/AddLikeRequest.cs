using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts.DTO
{
    public class AddLikeRequest
    {
        public Guid BlogId { get; set; }
        public Guid UserId { get; set; }

        public Like ToLike()
        {
            return new Like
            {
                LikeId = Guid.NewGuid(),
                BlogId = this.BlogId,
                UserId = this.UserId,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
