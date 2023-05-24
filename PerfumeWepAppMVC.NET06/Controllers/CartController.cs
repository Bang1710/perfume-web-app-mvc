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

        [HttpPost]
        public IActionResult AddToCart(string Proudct_ID, string Product_Quantity)
        {
            var product = _context.Products.Where(p => p.Product_ID == Proudct_ID).FirstOrDefault();

            var userid = HttpContext.Session.GetInt32("UserId");

            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.User_ID == userid);
            
            // Kiếm tra nếu userid null nghĩa là người dùng chưa đăng, hiển thị thông báo, và điều hướng đến trang Login
            if (userid == null)
            {
                TempData["LoginToAddCart"] = "Hãy đăng nhập vào tài khoản để thêm sản phẩm vào giỏ hàng của bạn";
                return RedirectToAction("Login", "Account");
            } 

            // Trường hợp user đã đăng nhập, nhưng cart lại chưa có, thì sẽ tạo mới
            if (cart == null)
            {
                cart = new Cart()
                {
                    User_ID = (int)userid,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
            }

            // Lấy ra CartItem của sản phẩm ta đang định thêm vào
            var cartItem = cart.CartItems.FirstOrDefault(c => c.Product_ID == Proudct_ID);

            // Trường hợp đã có thì ta sẽ tăng thêm số lượng của sản phẩm ta thêm vào giỏ hàng
            if (cartItem != null)
            {
                cartItem.Quantity += int.Parse(Product_Quantity);
            } // Trường hợp chưa có CartItem cho sp đó thì ta sẽ tạo mới
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
            // Sau cùng ta sẽ lưu những dữ liệu xuống db
            _context.SaveChanges();

            getCountCartItem((int)userid);


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
                getUserIDAndUserName((int)userId);
                getCountCartItem((int)userId);
            }

            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.User_ID == userId);

            if (cart == null)
            {
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
                int cartItemCount = _context.CartItems.Count(c => c.Cart_ID == cart.Cart_ID);
                ViewBag.CountCart = cartItemCount;
            }
            TempData["InforSuccessRemove"] = "Bạn đã xóa sản phẩm khỏi giỏ hàng thành công";

            return RedirectToAction("ViewCart", "Cart");
        }
    }
}
