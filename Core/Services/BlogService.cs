using Core.Interfaces;
using Core.Interfaces.DTO;
using Core.Domain.Entities;

namespace Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogResponse> AddBlog(AddBlogRequest? blogAddRequest)
        {
            if (blogAddRequest == null)
                throw new ArgumentNullException(nameof(blogAddRequest));

            if (string.IsNullOrEmpty(blogAddRequest.BlogTitle))
                throw new ArgumentException("Blog title cannot be null or empty");

            List<Blog> existingBlogs = await _blogRepository.GetAllBlogs();
            if (existingBlogs.Any(b => b.BlogTitle == blogAddRequest.BlogTitle))
                throw new ArgumentException("Duplicate blog title");

            Blog blog = blogAddRequest.ToBlog();
            Blog addedBlog = await _blogRepository.AddBlog(blog);
            return addedBlog.ToBlogResponse();
        }

        public async Task<List<BlogResponse>> GetAllBlogs()
        {
            List<Blog> blogs = await _blogRepository.GetAllBlogs();
            return blogs.Select(b => b.ToBlogResponse()).ToList();
        }

        public async Task<BlogResponse?> GetBlogById(Guid? blogId)
        {
            if (blogId == null) return null;
            Blog? blog = await _blogRepository.GetBlogById(blogId.Value);
            return blog?.ToBlogResponse();
        }

        public async Task<BlogResponse> UpdateBlog(UpdateBlogRequest? updateBlogRequest)
        {
            if (updateBlogRequest == null)
                throw new ArgumentNullException(nameof(updateBlogRequest));

            Blog? existingBlog = await _blogRepository.GetBlogById(updateBlogRequest.BlogId);
            if (existingBlog == null)
                throw new ArgumentException("Blog does not exist");

            existingBlog.BlogTitle = updateBlogRequest.BlogTitle;
            existingBlog.BlogContent = updateBlogRequest.BlogContent;
            existingBlog.UpdatedAt = DateTime.UtcNow;

            Blog updated = await _blogRepository.UpdateBlog(existingBlog);
            return updated.ToBlogResponse();
        }

        public async Task<bool> DeleteBlog(Guid? blogId)
        {
            if (blogId == null)
                throw new ArgumentNullException(nameof(blogId));
            return await _blogRepository.DeleteBlog(blogId.Value);
        }
    }
}
