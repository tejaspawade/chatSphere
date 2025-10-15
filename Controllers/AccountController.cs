using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class AccountController: Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var existing = await _userService.GetByUsernameAsync(user.Username);
            if (existing != null)
            {
                ViewBag.Error = "Username already exists";
                return View();
            }

            await _userService.RegisterAsync(user.Username, user.Email, user.PasswordHash);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userService.AuthenticateAsync(username, password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id);
                return RedirectToAction("Index", "Chat");
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
