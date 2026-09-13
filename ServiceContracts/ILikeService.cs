using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts
{
    public interface ILikeService
    {
        int GetLikeCountByBlogId(Guid blogId);
        bool HasUserLikedBlog(Guid blogId, Guid UserId);
        LikeResponse AddLike(AddLikeRequest? addLikeRequest);
        bool RemoveLike(Guid blogId, Guid userId);
    }
}
