using Core.Interfaces.DTO;

namespace Core.Interfaces
{
    public interface ILikeService
    {
        int GetLikeCountByBlogId(Guid blogId);
        bool HasUserLikedBlog(Guid blogId, Guid UserId);
        LikeResponse AddLike(AddLikeRequest? addLikeRequest);
        bool RemoveLike(Guid blogId, Guid userId);
    }
}
