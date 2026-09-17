using Core.Interfaces.DTO;

namespace Core.Interfaces
{
    public interface ILikeService
    {
        Task<int> GetLikeCountByBlogId(Guid blogId);
        Task<bool> HasUserLikedBlog(Guid blogId, Guid UserId);
        Task<LikeResponse> AddLike(AddLikeRequest? addLikeRequest);
        Task<bool> RemoveLike(Guid blogId, Guid userId);
    }
}
