using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeWebApp.NET06.Data;
using PerfumeWepAppMVC.NET06.Models;

namespace PerfumeWepAppMVC.NET06.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PerfumeDBContext _context;

        public HomeController(ILogger<HomeController> logger, PerfumeDBContext context)
        {
            _context = context;
            _logger = logger;
        }

        public void getCountCartItem(int id)
        {
            //Lấy ra Cart của userid đó
            Cart userCart = _context.Carts.FirstOrDefault(c => c.User_ID == id);

            //Kiểm tra nếu khác null thì thực hiện việc, lấy số lượng cartItem và lưu vào ViewBag để truyền lại cho _Layout
            if (userCart != null)
            {
                int cartItemCount = _context.CartItems.Count(c => c.Cart_ID == userCart.Cart_ID);
                HttpContext.Session.SetInt32("count", cartItemCount);
                ViewBag.CountCart = HttpContext.Session.GetInt32("count");
            }
        }

        public void getUserIDAndUserName(int id)
        {
            var userName = _context.Users.Where(u => u.User_ID == id).FirstOrDefault();
            ViewBag.UserID = id;
            ViewBag.UserName = userName.User_Name;
        }

        public IActionResult Index()
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null)
            {
                getUserIDAndUserName((int)userid);
                getCountCartItem((int)userid);
            }

            var ListproductTrending = _context.Products.Where(p => p.Product_IsTrending == true).Select(p =>
                new
                {
                    ProductID = p.Product_ID,
                    ProductName = p.Product_Name,
                    CategoryName = p.Category.Category_Name,
                    ProductPrice = p.Product_Price,
                    ProductGender = p.Product_Gender,
                }    
            ).ToList().Take(5);
            ViewBag.listTrending = ListproductTrending;

            var ListproductRecommend = _context.Products.Where(p => p.Product_IsRecommend == true).Select(p =>
                new 
                {
                    ProductID = p.Product_ID,
                    ProductName = p.Product_Name,
                    CategoryName = p.Category.Category_Name,
                    ProductPrice = p.Product_Price,
                    ProductGender = p.Product_Gender,
                }).ToList().Take(5);

            ViewBag.listRecommend = ListproductRecommend;
            return View();
        }
    }
}