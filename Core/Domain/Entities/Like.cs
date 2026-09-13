namespace Core.Domain.Entities
{
    public class Like
    {
        public Guid LikeId { get; set; }
        public Guid BlogId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
