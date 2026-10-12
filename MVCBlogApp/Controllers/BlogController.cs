using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Interfaces.DTO;
using System.Security.Claims;

namespace MVCBlog.Presentation.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly ILikeService _likeService;

        private bool isSuperAdmin()
        {
            return User.IsInRole("SuperAdmin");
        }

        private bool isAdmin()
        {
            return User.IsInRole("Admin") || User.IsInRole("SuperAdmin");
        }

        private bool isLoggedIn()
        {
            return User.Identity?.IsAuthenticated == true;
        }

        private bool CanEditBlog(Guid blogOwnerId)
        {
            if (User.IsInRole("Admin") || User.IsInRole("SuperAdmin")) return true;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return User.IsInRole("User") && userId == blogOwnerId.ToString();
        }

        public BlogController(IBlogService blogService, ICommentService commentService, ILikeService likeService)
        {
            _blogService = blogService;
            _commentService = commentService;
            _likeService = likeService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;
            List<BlogResponse> allblogs = await _blogService.GetAllBlogs();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(allblogs.Count / (double)pageSize);

            List<BlogResponse> blogsToShow = allblogs.Skip((page - 1) * pageSize).Take(pageSize).ToList(); 

            return View(blogsToShow);
        }

        public async Task<IActionResult> Details(Guid id, int commentsToShow = 3)
        {
            BlogResponse? blog = await _blogService.GetBlogById(id);

            if (blog == null)
            {
                return NotFound();
            }

            ViewBag.LikeCount = await _likeService.GetLikeCountByBlogId(id);
            
            List<CommentResponse> allComments = await _commentService.GetCommentsByBlogId(id);
            ViewBag.Comments = allComments.Take(commentsToShow).ToList();
            ViewBag.TotalComments = allComments.Count;
            ViewBag.CommentsToShow = commentsToShow;

            List<BlogResponse> allBlogs = await _blogService.GetAllBlogs();
            ViewBag.MorePosts = allBlogs.Where(b => b.BlogId != id).OrderBy(b => Guid.NewGuid()).Take(3).ToList();

            string? userIdString = HttpContext.Session.GetString("UserId");
            ViewBag.HasLiked = !string.IsNullOrEmpty(userIdString) && await _likeService.HasUserLikedBlog(id, Guid.Parse(userIdString));

            if (isAdmin())
            {
                ViewBag.Likers = await _likeService.GetLikersByBlogid(id);
            }

            return View(blog);
        }

        public IActionResult Create()
        {
            if (!isAdmin()) return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddBlogRequest addBlogRequest)
        {
            if (!isAdmin()) return RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                return View(addBlogRequest);
            }

            string? userIdString = HttpContext.Session.GetString("UserId");
            addBlogRequest.UserId = Guid.Parse(userIdString!);
            try
            {
                BlogResponse blog = await _blogService.AddBlog(addBlogRequest);
                return RedirectToAction("Details", new { id = blog.BlogId });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(addBlogRequest);
            }
        }

        public async Task<IActionResult> Update(Guid id)
        {
            BlogResponse? blog = await _blogService.GetBlogById(id);

            if (blog == null)
            {
                return NotFound();
            }

            if (!CanEditBlog(blog.UserId)) return RedirectToAction("Index");

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
            BlogResponse? existingBlog = await _blogService.GetBlogById(id);

            if (existingBlog == null) return NotFound();

            if (!CanEditBlog(existingBlog.UserId)) return RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                return View(updateBlogRequest);
            }

            try
            {
                updateBlogRequest.BlogId = id;
                await _blogService.UpdateBlog(updateBlogRequest);

                return RedirectToAction("Details", new { id });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(updateBlogRequest);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!isAdmin()) return RedirectToAction("Index");
            await _blogService.DeleteBlog(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Like(Guid blogId)
        {
            string? userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Account");
            }

            Guid userId = Guid.Parse(userIdString);
            bool alreadyLiked = await _likeService.HasUserLikedBlog(blogId, userId);

            if (alreadyLiked)
            {
                await _likeService.RemoveLike(blogId, userId);
            }
            else
            {
                try
                {
                    await _likeService.AddLike(new AddLikeRequest { BlogId = blogId, UserId = userId });
                }
                catch (ArgumentException)
                {
                    // Race condition or stale check - already liked, ignore
                }
            }

            return RedirectToAction("Details", new {id =  blogId});
        }

        [HttpPost]
        public async Task<IActionResult> Comment(Guid blogId,string commentText)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (string.IsNullOrEmpty(userId.ToString()))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                await _commentService.AddComment(new AddCommentRequest
                {
                    BlogId = blogId,
                    UserId = userId,
                    CommentText = commentText
                });
            }
            catch (ArgumentException ex)
            {
                TempData["CommentError"] = ex.Message;
            }

            return RedirectToAction("Details", new { id = blogId });
        }

        public async Task<IActionResult> MyPosts()
        {
            if (!isLoggedIn()) return RedirectToAction("Login", "Account");

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return View(userId);
        }
    }
}
