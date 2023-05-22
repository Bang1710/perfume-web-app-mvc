using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
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
                TempData["LoginToAddCart"] = "Hãy đăng nhập vào tài khoản để đặt sản phẩm của chúng tôi";
                return RedirectToAction("Login", "Account");
            } else
            {
                ViewBag.CountCart = HttpContext.Session.GetInt32("count");
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

            Cart userCart = _context.Carts.FirstOrDefault(c => c.User_ID == userid);

            if (userCart != null)
            {
                int cartItemCount = _context.CartItems.Count(c => c.Cart_ID == userCart.Cart_ID);
                ViewBag.CountCart = cartItemCount;
            }


            TempData["AddToCartSuccess"] = "Đã thêm vào giỏ hàng thành công";
            return RedirectToRoute("MyCustomRoute", new { id = product.Product_ID });
            //return RedirectToAction("ViewCart");
            //return RedirectToPage("/chi-tiet-san-pham", new { id = product.Product_ID });
        }

        [Route("gio-hang/chi-tiet-gio-hang-cua-ban")]        
        public IActionResult ViewCart()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                TempData["LoginToViewCart"] = "Hãy đăng nhập vào tài khoản để xem giỏ hàng của bạn";
                return RedirectToAction("Login", "Account");
            } else
            {
                var userName = _context.Users.Where(u => u.User_ID == userId).FirstOrDefault();
                ViewBag.UserID = userId;
                ViewBag.UserName = userName.User_Name;
                Cart userCart = _context.Carts.FirstOrDefault(c => c.User_ID == userId);

                if (userCart != null)
                {
                    int cartItemCount = _context.CartItems.Count(c => c.Cart_ID == userCart.Cart_ID);
                    ViewBag.CountCart = cartItemCount;
                }
            }

            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.User_ID == userId);

            if (cart == null)
            {
                //ViewData["ProductCart"] = "Giỏ hàng của bạn trống !";
                return View();
            }
            return View(cart.CartItems.ToList());
        }

        [HttpPost]
        public IActionResult RemoveProductFromCart(string Product_ID)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            Cart cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.User_ID == userId);

            if (cart != null)
            {
                CartItem cartItem = cart.CartItems.FirstOrDefault(c => c.Product_ID == Product_ID);
                if (cartItem != null)
                {
                    _context.CartItems.Remove(cartItem);
                    _context.SaveChanges();
                }
            }

            // Cập nhật số lượng sản phẩm trong giỏ hàng
            int cartItemCount = cart.CartItems.Count;
            ViewBag.CountCart = cartItemCount;

            //if (cartItemCount == 0)
            //{
            //    ViewData["ProductCart"] = "Giỏ hàng của bạn trống !";
            //}

            return RedirectToAction("ViewCart", "Cart");
        }
    }
}
