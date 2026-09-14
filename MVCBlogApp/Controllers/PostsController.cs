using Microsoft.AspNetCore.Mvc;
using MvcBlogSite.Services;
using MvcBlogSite.Models;

namespace MvcBlogSite.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostServices _postServices;

        public PostsController(PostServices postServices)
        {
            _postServices = postServices;
        }

        public async Task<IActionResult> Index()
        {
            List<Post> posts = await _postServices.GetAllPostsAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            List<Post> posts = await _postServices.GetAllPostsAsync();
            Post post = posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Create(Post post) 
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }
            await _postServices.AddPostAsync(post);
            return RedirectToAction("Details", new { id = post.Id }); 
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            List<Post> posts = await _postServices.GetAllPostsAsync();
            Post updatePost = posts.FirstOrDefault(p => p.Id == id);

            if (updatePost == null)
            {
                return NotFound();
            }

            return View(updatePost);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Post post)
        {
            ModelState.Remove("Author");

            if (!ModelState.IsValid)
            {
                return View(post);
            }

            await _postServices.UpdatePostAsync(id, post);
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _postServices.DeletePostAsync(id);
            return RedirectToAction("Index");
        }
    }
}
