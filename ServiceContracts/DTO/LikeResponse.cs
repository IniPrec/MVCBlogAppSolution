using Entities;

namespace ServiceContracts.DTO
{
    public class LikeResponse
    {
        public Guid LikeId { get; set; }
        public Guid BlogId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public static class LikeResponseExtensions
    {
        public static LikeResponse ToLikeResponse(this Like like)
        {
            return new LikeResponse
            {
                LikeId = like.LikeId,
                BlogId = like.BlogId,
                UserId = like.UserId,
                CreatedAt = like.CreatedAt,
            };
        }
    }
}
