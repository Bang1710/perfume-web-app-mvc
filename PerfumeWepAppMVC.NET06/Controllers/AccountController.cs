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
        [Route("dang-nhap")]
        public IActionResult Login()
        {
            // truy cập bằng phương thức get
            return View();
        }

        [HttpPost]
        [Route("dang-nhap")]
        public IActionResult Login(User user)
        {
            // Lấy ra user đã được lưu trong csdl
            var userLogin = _context.Users.Where(u => u.User_Email == user.User_Email && u.User_Password == user.User_Password).FirstOrDefault();
            if (userLogin != null)
            {
                // Lưu userid vào Session
                HttpContext.Session.SetInt32("UserId", userLogin.User_ID);

                //Lấy ra userid được lưu trong Session
                var userid = HttpContext.Session.GetInt32("UserId");

                //getCountCartItem(user.User_ID);
                // Đăng nhập thành công, chuyển hướng về Action index của controller home
                return RedirectToAction("Index", "Home");

            }
            //Trường hợp đăng nhập không thành công sẽ, truyển ViewData hiển thị thông báo lỗi ở View Login
            ViewData["ErrorMessage"] = "Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin đăng nhập.";

            // Lưu lại dữ liệu người dùng nhập vào khi đăng nhập, mà bị lỗi
            TempData["User_Email"] = user.User_Email;
            TempData["User_Password"] = user.User_Password;

            // Trả về view hiện tại, View Login
            return View();
        }
        
        [Route("dang-xuat")]
        public IActionResult Logout()
        {
            //Remove session userid của người dùng
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ
        }


        //Account/Register
        [HttpGet]
        [Route("dang-ky")]
        public IActionResult Register()
        {
            //Truy cập bằng phương thức get
            return View();
        }


        [HttpPost]
        [Route("dang-ky")]
        public IActionResult Register(User user, string User_ConfirmPassword)
        {
            // Kiểm tra tên đăng nhập đã tồn tại hay chưa
            bool userNameExist = _context.Users.Any(u => u.User_Name == user.User_Name);
            bool isValid = true;

            //  Nếu đã tồn tại thì hiển thị thông báo thông qua ModelState và hiển thị ở View thông qua Html.ValidationMessageFor
            if (userNameExist)
            {
                ModelState.AddModelError("User_Name", "Tên người dùng đã tồn tại, hãy nhập tên đăng nhập khác");
                // Biến này để biến người dùng đang nhập dữ liệu sai, sau cùng sẽ check để hiển thị thông báo và không lưu xuống csdl
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

            // Kiểm tra nếu người dùng nhập dữ liệu đúng thì tạo đối tượng User, insert data và lưu xuống db
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

                // Sau khi đăng ký thành công thì Lưu userid vào Session 
                HttpContext.Session.SetInt32("UserId", userRegister.User_ID);
                //getCountCartItem(user.User_ID);

                // Chuyển hướng tới trang đăng nhập hoặc trang chính
                return RedirectToAction("Index", "Home");
            }

            // Hiển thị lại view với thông tin người dùng và thông báo lỗi
            return View(user);
        }

       

    }

}
