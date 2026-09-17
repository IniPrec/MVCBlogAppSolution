using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Interfaces.DTO;

namespace MVCBlogApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(registerRequest);
            }

            try
            {
                UserResponse user = await _authService.Register(registerRequest);

                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("UserName", user.UserName ?? string.Empty);
                HttpContext.Session.SetString("Role", user.Role ?? string.Empty);

                return RedirectToAction("Index", "Blog");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(registerRequest);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            try
            {
                UserResponse? user = await _authService.Login(loginRequest);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return View(loginRequest);
                }

                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("UserName", user.UserName ?? string.Empty);
                HttpContext.Session.SetString("Role", user.Role ?? string.Empty);

                return RedirectToAction("Index", "Blog");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(loginRequest);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Blog");
        }
    }
}
