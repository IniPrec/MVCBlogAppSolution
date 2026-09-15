using Microsoft.AspNetCore.Mvc;

namespace MVCBlogApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
