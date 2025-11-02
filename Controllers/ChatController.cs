using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserService _userService;

        public ChatController(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            string? userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var user = await _userService.GetByIdAsync(userId);
            ViewBag.Username = user?.Username ?? "Unknown";
            ViewBag.UserId = user?.Id ?? "";

            return View();
        }
    }
}
