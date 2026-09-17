using Core.Domain.Entities;

namespace Core.Interfaces.DTO
{
    public interface ILikeRepository
    {
        Task<Like> AddLike(Like like);
        Task<int> GetLikeCountByBlogId(Guid blogId);
        Task<Like?> GetLike(Guid blogId, Guid userId);
        Task<bool> RemoveLike(Guid blogId, Guid userId);
    }
}
