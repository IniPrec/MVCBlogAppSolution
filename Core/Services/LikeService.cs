using Application.Interfaces;
using Application.Interfaces.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly UserManager<ApplicationUser> _userManager;

		public LikeService(ILikeRepository likeRepository, UserManager<ApplicationUser> userManager)
        {
            _likeRepository = likeRepository;
			_userManager = userManager;
		}

        public async Task<LikeResponse> AddLike(AddLikeRequest? addLikeRequest)
        {
            if (addLikeRequest == null)
            {
                throw new ArgumentNullException(nameof(addLikeRequest));
            }

            Like? existingLike = await _likeRepository.GetLike(addLikeRequest.BlogId, addLikeRequest.UserId);
            if (existingLike != null)
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

        public async Task<List<string>> GetLikersByBlogid(Guid blogId)
        {
            List<Guid> userIds = await _likeRepository.GetUserIdsByBlogId(blogId);
            List<string> names = new List<string>();

            foreach (var userId in userIds)
            {
                ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
				names.Add(user?.UserName ?? "Unknown");
            }

            return names;
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
