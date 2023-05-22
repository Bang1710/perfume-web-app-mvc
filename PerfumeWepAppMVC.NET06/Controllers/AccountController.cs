using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

                Cart userCart = _context.Carts.FirstOrDefault(c => c.User_ID == userid);

                if (userCart != null)
                {
                    int cartItemCount = _context.CartItems.Count(c => c.Cart_ID == userCart.Cart_ID);
                    HttpContext.Session.SetInt32("count", cartItemCount);
                    ViewBag.CountCart = HttpContext.Session.GetInt32("count");
                }

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
        public IActionResult Register(User user, string User_ConfirmPassword)
        {
            bool userNameExist = _context.Users.Any(u => u.User_Name == user.User_Name);
            bool isValid = true;
            if (userNameExist)
            {
                ModelState.AddModelError("User_Name", "Tên người dùng đã tồn tại, hãy nhập tên đăng nhập khác");
                isValid = false;
            }

            bool userEmailExist = _context.Users.Any(u => u.User_Email == user.User_Email);
            if (userEmailExist)
            {
                ModelState.AddModelError("User_Email", "Email đã được đăng ký, hãy nhập email khác, hoặc đăng nhập vào tài khoản của bạn");
                isValid = false;
            }
            else if (user.User_Email == null)
            {
                ModelState.AddModelError("User_Email", "Email không được bỏ trống");
                isValid = false;
            }

            if (user.User_Password != User_ConfirmPassword)
            {
                ViewData["ErrorUserPassword"] = "Mật khẩu nhập lại chưa chính xác";
                isValid = false;
            }

            if (isValid)
            {
                User userRegister = new User()
                {
                    User_Name = user.User_Name,
                    User_Email = user.User_Email,
                    User_Password = user.User_Password,
                };
                // Lưu thông tin người dùng vào cơ sở dữ liệu
                _context.Users.Add(userRegister);
                _context.SaveChanges();

                // Lưu userid vào Session
                HttpContext.Session.SetInt32("UserId", userRegister.User_ID);
                Cart userCart = _context.Carts.FirstOrDefault(c => c.User_ID == user.User_ID);

                if (userCart != null)
                {
                    int cartItemCount = _context.CartItems.Count(c => c.Cart_ID == userCart.Cart_ID);
                    HttpContext.Session.SetInt32("count", cartItemCount);
                    ViewBag.CountCart = HttpContext.Session.GetInt32("count");
                }

                // Chuyển hướng tới trang đăng nhập hoặc trang chính
                return RedirectToAction("Index", "Home");
            }

            // Hiển thị lại view với thông tin người dùng và thông báo lỗi
            return View(user);
        }

    }

}
