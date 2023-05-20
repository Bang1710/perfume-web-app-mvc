using Microsoft.AspNetCore.Mvc;
using PerfumeWebApp.NET06.Data;
using PerfumeWepAppMVC.NET06.Models;

namespace PerfumeWepAppMVC.NET06.Controllers
{
    public class AccountController : Controller
    {
        public readonly ILogger<AccountController> _logger;
        public readonly PerfumeDBContext _context;

        public AccountController(ILogger<AccountController> logger, PerfumeDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Account/login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var userLogin = _context.Users.Where(u => u.User_Email == user.User_Email && u.User_Password == user.User_Password).FirstOrDefault();
            if (userLogin != null)
            {
                // Lưu UserId vào Session
                HttpContext.Session.SetInt32("UserId", userLogin.User_ID);

                var userid = HttpContext.Session.GetInt32("UserId");

                return RedirectToAction("Index", "Home");

            }

            ViewData["ErrorMessage"] = "Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin đăng nhập.";
            TempData["User_Email"] = user.User_Email;
            TempData["User_Password"] = user.User_Password;

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login", "Account"); // Chuyển hướng đến trang đăng nhập
        }


        //Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên đăng nhập đã tồn tại hay chưa
                bool isUsernameTaken = _context.Users.Any(u => u.User_Name == model.User_Name || u.User_Email == model.User_Email);
                if (isUsernameTaken)
                {
                    ModelState.AddModelError("", "Username is already taken.");
                    return View(model);
                }

                // Lưu thông tin người dùng vào cơ sở dữ liệu
                _context.Users.Add(model);
                _context.SaveChanges();

                // Lưu userid vào Session
                HttpContext.Session.SetInt32("UserId", model.User_ID);

                // Chuyển hướng tới trang đăng nhập hoặc trang chính
                return RedirectToAction("Login", "Account");
            }

            // Trường hợp dữ liệu không hợp lệ, hiển thị lại form đăng ký với thông báo lỗi
            return View(model);
        }
    }

}
