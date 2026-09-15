using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Interfaces.DTO;

namespace MVCBlogApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            List<BlogResponse> blogs = await _blogService.GetAllBlogs();

            return View(blogs);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            BlogResponse? blog = await _blogService.GetBlogById(id);

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddBlogRequest addBlogRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(addBlogRequest);
            }

            addBlogRequest.UserId = Guid.Parse("3592D482-D0E3-4ED6-BE07-4E26980967B9");

            BlogResponse blog = await _blogService.AddBlog(addBlogRequest);
            return RedirectToAction("Details", new { id = blog.BlogId });
        }

        public async Task<IActionResult> Update(Guid id)
        {
            BlogResponse? blog = await _blogService.GetBlogById(id);

            if (blog == null)
            {
                return NotFound();
            }

            UpdateBlogRequest updateBlogRequest = new UpdateBlogRequest
            {
                BlogId = blog.BlogId,
                BlogTitle = blog.BlogTitle,
                BlogContent = blog.BlogContent
            };

            return View(updateBlogRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateBlogRequest updateBlogRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(updateBlogRequest);
            }
            updateBlogRequest.BlogId = id;
            await _blogService.UpdateBlog(updateBlogRequest);
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _blogService.DeleteBlog(id);
            return RedirectToAction("Index");
        }
    }
}
