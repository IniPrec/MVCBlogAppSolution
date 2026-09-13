using ServiceContracts;
using ServiceContracts.DTO;
using Entities;

namespace Services
{
    public class BlogService : IBlogService
    {
        private readonly List<Blog> _blogs;

        public BlogService()
        {
            _blogs = new List<Blog>();
        }

        public BlogResponse AddBlog(AddBlogRequest? blogAddRequest)
        {
            if (blogAddRequest == null)
            {
                throw new ArgumentNullException(nameof(blogAddRequest), "Blog request cannot be null");
            }

            if (blogAddRequest.BlogTitle == null)
            {
                throw new ArgumentException("Blog title cannot be null", nameof(blogAddRequest.BlogTitle));
            }

            if (blogAddRequest.BlogTitle ==  string.Empty)
            {
                throw new ArgumentException("Blog title cannot be empty", nameof(blogAddRequest.BlogTitle));
            }

            if (_blogs.Where(temp => temp.BlogTitle == blogAddRequest.BlogTitle).Any())
            {
                throw new ArgumentException("Blog title already exists", nameof(blogAddRequest.BlogTitle));
            }

            Blog blog = blogAddRequest.ToBlog();

            blog.BlogId = Guid.NewGuid();

            _blogs.Add(blog);

            return blog.ToBlogResponse();
        }


        public List<BlogResponse> GetAllBlogs()
        {
            return _blogs.Select(blog => blog.ToBlogResponse()).ToList();
        }

        public BlogResponse? GetBlogById(Guid? blogId)
        {
            if (blogId == null)
                return null;

            Blog? blog_response_from_list = _blogs.FirstOrDefault(temp => temp.BlogId == blogId);

            if (blog_response_from_list == null)
                return null;

            return blog_response_from_list.ToBlogResponse();
        }

        public BlogResponse UpdateBlog(UpdateBlogRequest? updateBlogRequest)
        {
            if (updateBlogRequest == null)
            {
                throw new ArgumentNullException(nameof(updateBlogRequest), "Update request cannot be null");
            }

            Blog? existingBlog = _blogs.FirstOrDefault(temp => temp.BlogId == updateBlogRequest.BlogId);

            if (existingBlog == null)
            {
                throw new ArgumentException("Blog with given BlogId does not exist");
            }

            existingBlog.BlogTitle = updateBlogRequest.BlogTitle;
            existingBlog.BlogContent = updateBlogRequest.BlogContent;
            existingBlog.UpdatedAt = DateTime.UtcNow;

            return existingBlog.ToBlogResponse();
        }

        public bool DeleteBlog(Guid? blogId)
        {
            if (blogId == null)
            {
                throw new ArgumentNullException(nameof(blogId), "Blog Id cannot be null");
            }

            Blog? blog = _blogs.FirstOrDefault(temp => temp.BlogId == blogId);

            if (blog == null)
            {
                return false;
            }

            _blogs.Remove(blog);
            return true;
        }
    }
}
