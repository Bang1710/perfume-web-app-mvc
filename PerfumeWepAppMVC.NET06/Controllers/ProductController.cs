using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeWebApp.NET06.Data;
using PerfumeWepAppMVC.NET06.Models;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;

namespace PerfumeWepAppMVC.NET06.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly PerfumeDBContext _context;

        public ProductController(ILogger<ProductController> logger, PerfumeDBContext context)
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

        public void SetValueViewBag()
        {
            var categoryNames = _context.Categories.ToList();
            ViewBag.CategoryNames = categoryNames;

            var genderProduct = _context.Products.Select(p => p.Product_Gender).Distinct().ToList();
            ViewBag.GenderProduct = genderProduct;

            var originProduct = _context.Products.Select(p => p.Product_Origin).Distinct().ToList();
            ViewBag.OriginProduct = originProduct;

            var volumeProduct = _context.Products.Select(p => p.Product_Volume).Distinct().ToList();
            ViewBag.VolumeProduct = volumeProduct;
        }

        [Route("danh-sach-san-pham/")]
        public IActionResult Index(int page = 1, int pageSize = 8)
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null)
            {
                getUserIDAndUserName((int)userid);
                getCountCartItem((int)userid);
            }
            SetValueViewBag();
            var listAllProduct = _context.Products
                                            .OrderBy(p => p.Product_ID)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(_context.Products.Count() / (double)pageSize);
            return View(listAllProduct);
        }

        [HttpGet]
        [Route("Danh-sach-nuoc-hoa-theo-thuong-hieu/{brandID?}")]
        public IActionResult ViewProductByBrand(string brandID, string? priceSortOrder, List<string>? gender, List<string>? capacity, List<string>? original)
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null)
            {
                getUserIDAndUserName((int)userid);
                getCountCartItem((int)userid);
            }

            SetValueViewBag();


            var CategoryName = _context.Categories.Where(c => c.Category_ID == brandID).Select(c => c.Category_Name).FirstOrDefault();
            ViewBag.CategoryName = CategoryName.ToString();

            var products = _context.Products.ToList();

            if (brandID != null)
            {
                products = products.Where(p => p.Category_ID == brandID).ToList();
            }

            products = FilterAndSortProducts(products, priceSortOrder, null, gender, capacity, original);


            ViewBag.PriceSortOrder = priceSortOrder;
            ViewBag.SelectedGenders = gender;
            ViewBag.SelectedCapacities = capacity;
            ViewBag.SelectedOriginals = original;

            return View(products);
        }

        [HttpGet]
        [Route("Danh-sach-nuoc-hoa-theo-gioi-tinh/{gender?}")]
        public IActionResult ViewProductByGender(string gender, string? priceSortOrder, List<string>? brand, List<string>? capacity, List<string>? original)
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null)
            {
                getUserIDAndUserName((int)userid);
                getCountCartItem((int)userid);
            }

            SetValueViewBag();


            ViewBag.ProductGender = gender.ToUpper();

            var products = _context.Products.ToList();

            if (gender != null)
            {
                products = products.Where(p => p.Product_Gender == gender).ToList();
            }

            products = FilterAndSortProducts(products, priceSortOrder, brand, null, capacity, original);


            ViewBag.PriceSortOrder = priceSortOrder;
            ViewBag.SelectedBrands = brand;
            ViewBag.SelectedCapacities = capacity;
            ViewBag.SelectedOriginals = original;

            return View(products);
        }

        [HttpGet]
        [Route("Danh-sach-nuoc-hoa-moi/")]
        public IActionResult ViewProductNew(string? priceSortOrder, List<string>? brand, List<string>? gender, List<string>? capacity, List<string>? original)
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null)
            {
                getUserIDAndUserName((int)userid);
                getCountCartItem((int)userid);
            }

            SetValueViewBag();

            var products = _context.Products.Where(p => p.Product_IsNew == true).ToList();



            products = FilterAndSortProducts(products, priceSortOrder, brand, gender, capacity, original);


            ViewBag.PriceSortOrder = priceSortOrder;
            ViewBag.SelectedBrands = brand;
            ViewBag.SelectedGenders = gender;
            ViewBag.SelectedCapacities = capacity;
            ViewBag.SelectedOriginals = original;

            return View(products);
        }



        [HttpGet]
        [Route("chi-tiet-san-pham/{id?}")]
        public ActionResult Detail(string? id)
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null)
            {
                getUserIDAndUserName((int)userid);
                getCountCartItem((int)userid);
            }

            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Product_ID == id);

            ViewBag.productById = product?.Category?.Category_Name;
            ViewBag.ProductSpec = _context.ProductSpecs.FirstOrDefault(p => p.Product_ID == id);

            var productRelated = _context.Products
                .Where(p => p.Category_ID == product.Category_ID && p.Product_ID != id)
                .Select(p => new
                {
                    ProductID = p.Product_ID,
                    ProductName = p.Product_Name,
                    CategoryName = p.Category.Category_Name,
                    ProductPrice = p.Product_Price,
                    ProductGender = p.Product_Gender,
                })
                .Take(4)
                .ToList();

            ViewBag.ProductRelated = productRelated;

            return View(product);
        }

        public string MessageStatus { get; set; }
        public string AlertMessage { get; set; }
        public string MessageStatusSearch { get; set; }


        private List<Product> FilterAndSortProducts(List<Product> products, string? priceSortOrder, List<string>? brand, List<string>? gender, List<string>? capacity, List<string>? original)
        {
            //Sort theo giá sản phẩm
            if (priceSortOrder != null && priceSortOrder.Any())
            {
                if (priceSortOrder == "asc")
                {
                    products = products.OrderBy(p => p.Product_Price).ToList();
                }
                else if (priceSortOrder == "desc")
                {
                    products = products.OrderByDescending(p => p.Product_Price).ToList();
                }
            }

            //Sort theo mã loại sp
            if (brand != null && brand.Any())
            {
                products = products.Where(p => brand.Contains(p.Category_ID)).ToList();
            }

            //Sort theo giới tính
            if (gender != null && gender.Any())
            {
                products = products.Where(p => gender.Contains(p.Product_Gender)).ToList();
            }

            //Sort theo dung tích
            if (capacity != null && capacity.Any())
            {
                products = products.Where(p => capacity.Contains(p.Product_Volume)).ToList();
            }

            //Sort theo Xuất xứ
            if (original != null && original.Any())
            {
                products = products.Where(p => original.Contains(p.Product_Origin)).ToList();
            }

            return products;
        }

        public IActionResult Filter(string? priceSortOrder, List<string>? brand, List<string>? gender, List<string>? capacity, List<string>? original)
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null)
            {
                getUserIDAndUserName((int)userid);
                getCountCartItem((int)userid);
            }

            SetValueViewBag();
            var products = _context.Products.ToList();

            products = FilterAndSortProducts(products, priceSortOrder, brand, gender, capacity, original);

            //Hiển thị thông báo
            if (products != null && products.Any())
            {
                MessageStatus = "Danh sách kết quả sau khi lọc";
                AlertMessage = "alert-success";
            }
            else
            {
                MessageStatus = "Không có sản phẩm nào phù hợp với kết quả lọc của bạn";
                AlertMessage = "alert-danger";
            }

            ViewBag.Message = MessageStatus;
            ViewBag.Alert = AlertMessage;

            ViewBag.PriceSortOrder = priceSortOrder;
            ViewBag.SelectedBrands = brand; // selectedBrands là danh sách các thương hiệu đã được chọn
            ViewBag.SelectedGenders = gender;
            ViewBag.SelectedCapacities = capacity;
            ViewBag.SelectedOriginals = original;


            return View(products);
        }

        public ActionResult Search(string? searchString, string? searchHistory, string? priceSortOrder, List<string>? brand, List<string>? gender, List<string>? capacity, List<string>? original)
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null)
            {
                getUserIDAndUserName((int)userid);
                getCountCartItem((int)userid);
            }

            SetValueViewBag();

            ViewBag.PriceSortOrder = priceSortOrder;
            ViewBag.SelectedBrands = brand; // selectedBrands là danh sách các thương hiệu đã được chọn
            ViewBag.SelectedGenders = gender;
            ViewBag.SelectedCapacities = capacity;
            ViewBag.SelectedOriginals = original;

            var stringSearchHistory = "";
            var products = _context.Products.ToList();

            if (searchString != null && searchString.Any())
            {
                products = _context.Products.Where(p => p.Product_Name.Contains(searchString)).ToList();
            }

            if (searchHistory != null && searchHistory.Any())
            {
                products = _context.Products.Where(p => p.Product_Name.Contains(searchHistory)).ToList();
                stringSearchHistory = Request.Query["searchHistory"].ToString();
                ViewBag.StringSearchHistory = stringSearchHistory;
            }

            products = FilterAndSortProducts(products, priceSortOrder, brand, gender, capacity, original);

            var result = (searchString != null) ? searchString : searchHistory;

            if (products != null && products.Any())
            {
                AlertMessage = "alert-success";
            }
            else
            {
                AlertMessage = "alert-danger";
            }

            if (!(products != null && !products.Any()))
            {
                ViewBag.Search = searchString;
            }

            MessageStatusSearch = result;
            ViewData["MessageSearch"] = MessageStatusSearch;
            ViewBag.ProductSearch = products;
            ViewBag.Alert = AlertMessage;
            return View(products);
        }


        


        






    }
}
