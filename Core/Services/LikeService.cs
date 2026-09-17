using Core.Interfaces;
using Core.Interfaces.DTO;
using Core.Domain.Entities;

namespace Core.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;   
        }

        public async Task<LikeResponse> AddLike(AddLikeRequest? addLikeRequest)
        {
            if (addLikeRequest == null)
            {
                throw new ArgumentNullException(nameof(addLikeRequest));
            }

            Like? existingLike = await _likeRepository.GetLike(addLikeRequest.BlogId, addLikeRequest.UserId);
            if (existingLike == null)
            {
                throw new ArgumentException("User already like this blog.");  
            }

            Like like = addLikeRequest.ToLike();
            Like addedLike = await _likeRepository.AddLike(like);

            return addedLike.ToLikeResponse();
        }

        public async Task<int> GetLikeCountByBlogId(Guid blogId)
        {
            return await _likeRepository.GetLikeCountByBlogId(blogId);
        }

        public async Task<bool> HasUserLikedBlog(Guid blogId, Guid userId)
        {
            Like? like = await _likeRepository.GetLike(blogId, userId);

            return like != null;
        }

        public async Task<bool> RemoveLike(Guid blogId, Guid userId)
        {
            return await _likeRepository.RemoveLike(blogId, userId);
        }
    }
}
