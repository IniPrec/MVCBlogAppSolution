using ServiceContracts;
using ServiceContracts.DTO;
using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;

namespace Services
{
    public class LikeService : ILikeService
    {
        private readonly List<Like> _likes;

        public LikeService()
        {
            _likes = new List<Like>();
        }

        public LikeResponse AddLike(AddLikeRequest? addLikeRequest)
        {
            if (addLikeRequest == null)
            {
                throw new ArgumentNullException(nameof(addLikeRequest), "Like request cannot be null");
            }

            bool alreadyLiked = _likes.Any(temp => temp.BlogId == addLikeRequest.BlogId && temp.UserId == addLikeRequest.UserId);

            if (alreadyLiked)
            {
                throw new ArgumentException("User has already like this blog");
            }

            Like like = addLikeRequest.ToLike();
            _likes.Add(like);

            return like.ToLikeResponse();
        }

        public int GetLikeCountByBlogId(Guid blogId)
        {
            return _likes.Count(temp => temp.BlogId == blogId);
        }

        public bool HasUserLikedBlog(Guid blogId, Guid userId)
        {
            return _likes.Any(temp => temp.BlogId == blogId && temp.UserId == userId);
        }

        public bool RemoveLike(Guid blogId, Guid userId)
        {
            Like? like = _likes.FirstOrDefault(temp => temp.BlogId == blogId && temp.UserId == userId);

            if (like == null)
            {
                return false;
            }

            _likes.Remove(like);
            return true;
        }
    }
}
