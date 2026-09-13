using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Interfaces.DTO;

namespace MVCBlogApp.Controllers
{
    public class BlogControllers
    {
        private readonly IBlogService _blogService;

        public BlogControllers(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            List<BlogResponse> blogs = _blogService.GetAllBlogs();

            return View(blogs);
        }

        public IActionResult Details(Guid id)
        {
            BlogResponse? blog = _blogService.GetBlogById(id);

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }
    }
}
