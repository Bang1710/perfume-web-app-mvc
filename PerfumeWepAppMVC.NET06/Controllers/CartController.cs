using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeWebApp.NET06.Data;
using PerfumeWepAppMVC.NET06.Models;

namespace PerfumeWepAppMVC.NET06.Controllers
{
    public class CartController : Controller
    {
        public readonly ILogger<CartController> _logger;
        public readonly PerfumeDBContext _context;

        public CartController(ILogger<CartController> logger, PerfumeDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult AddToCart(string Proudct_ID, string Product_Quantity)
        {
            var product = _context.Products.Where(p => p.Product_ID == Proudct_ID).FirstOrDefault();

            var userid = HttpContext.Session.GetInt32("UserId");

            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.User_ID == userid);

            //bool userId = _context.Users.Where( u => u.User_ID == UserID).Any();
            if (userid == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (cart == null)
            {
                cart = new Cart()
                {
                    User_ID = (int)userid,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(c => c.Product_ID == Proudct_ID);

            if (cartItem != null)
            {
                cartItem.Quantity += int.Parse(Product_Quantity);
            }
            else
            {
                cartItem = new CartItem()
                {
                    Product_ID = Proudct_ID,
                    Cart_ID = cart.Cart_ID,
                    Quantity = int.Parse(Product_Quantity),
                    Product_Name = product.Product_Name,
                    Product_Price = product.Product_Price,
                };
                cart.CartItems.Add(cartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            } else
            {
                var userName = _context.Users.Where(u => u.User_ID == userId).FirstOrDefault();
                ViewBag.UserID = userId;
                ViewBag.UserName = userName.User_Name;
            }

            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.User_ID == userId);

            if (cart == null)
            {
                ViewData["ProductCart"] = "Giỏ hàng của bạn trống !";
            }
            return View(cart.CartItems.ToList());

            //if (cart == null)
            //{
            //    // Không tìm thấy giỏ hàng
            //    //return View(new List<CartItem>());
            //    return View();
            //}

            //var ListcartItems = cart.CartItems.ToList();


            //ViewBag.CartItems = ListcartItems; // 

            ////return View(ListcartItems);
            //return View(ListcartItems);

        }
    }
}
