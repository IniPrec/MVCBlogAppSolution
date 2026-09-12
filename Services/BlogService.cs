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

        public AddBlogResponse AddBlog(AddBlogRequest? blogAddRequest)
        {
            /*
             * 1. Check if the blogAddRequest is not null
             * 2. Validate all properties of blogAddRequest
             * 3. Convert blogAddRequest from AddBlogRequest to Blog
             * 4. Generate a new Guid for the BlogId
             * 5. Add the blog to List<Blog> (in-memory data store)
             * 6. Return the AddBlogResponse object with the BlogId and BlogTitle
             */

            if (blogAddRequest == null)
            {
                throw new ArgumentNullException(nameof(blogAddRequest), "Blog request cannot be null");
            }

            if (blogAddRequest.BlogTitle == null)
            {
                throw new ArgumentException("Blog title cannot be null", nameof(blogAddRequest.BlogTitle));
            }

            Blog blog = blogAddRequest.ToBlog();

            blog.BlogId = Guid.NewGuid();

            _blogs.Add(blog);

            return blog.ToAddBlogResponse();
        }
    }
}
