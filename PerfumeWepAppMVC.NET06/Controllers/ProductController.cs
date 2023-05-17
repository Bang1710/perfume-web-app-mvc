using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeWebApp.NET06.Data;
using PerfumeWepAppMVC.NET06.Models;

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
        public IActionResult Index()
        {
            SetValueViewBag();
            var listAllProduct = _context.Products.ToList();
            //TempData["products"] = listAllProduct;
            return View(listAllProduct);
        }

        public string MessageStatus { get; set; }
        public string AlertMessage { get; set; }

        [HttpGet]
        [Route("loc-san-pham/")]
        public IActionResult Filter(string? priceSortOrder, List<string>? brand, List<string>? gender, List<string>? capacity, List<string>? original)
        {
            SetValueViewBag();
            var products = _context.Products.ToList();
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

            //Hiển thị thông báo
            if (products != null && products.Any())
            {
                MessageStatus = "Danh sách kết quả sau khi lọc";
                AlertMessage = "alert-success";
            } else
            {
                MessageStatus = "Không có sản phẩm nào phụ hợp với kết quả lọc của bạn";
                AlertMessage = "alert-danger";
            }
            ViewBag.Message = MessageStatus;
            ViewBag.Alert = AlertMessage;
            return View(products);
        }

        [HttpGet]
        [Route("chi-tiet-san-pham/{id?}")]
        public ActionResult Detail(string? id)
        {
            var product = _context.Products.Where(p => p.Product_ID == id).FirstOrDefault();

            var productCategoryName = _context.Products.Where(p => p.Product_ID == id).Select(p => p.Category.Category_Name).FirstOrDefault();
            ViewBag.productById = productCategoryName;

            var productCategoryID = product.Category_ID.ToString();

            var productSpec = _context.ProductSpecs.Where(p => p.Product_ID == id).FirstOrDefault();
            ViewBag.ProductSpec = productSpec;

            var productRelated = _context.Products.Where(p => p.Category_ID == productCategoryID && p.Product_ID != id).Select(p =>
                new
                {
                    ProductID = p.Product_ID,
                    ProductName = p.Product_Name,
                    CategoryName = p.Category.Category_Name,
                    ProductPrice = p.Product_Price,
                    ProductGender = p.Product_Gender,
                }).ToList().Take(4);

            ViewBag.ProductRelated = productRelated;
            return View(product);
        }

        public string MessageStatusSearch { get; set; }

        //[HttpGet]
        //[Route("tim-kiem/{searchString?}")]
        //public ActionResult Search(string? searchString)
        //{
        //    SetValueViewBag();
        //    var products = _context.Products.Where(p => p.Product_Name.Contains(searchString)).ToList();
        //    if (products != null && products.Any())
        //    {
        //        MessageStatusSearch = $"Danh sách sản phẩm sau khi tìm kiếm với từ khóa '{searchString}'";
        //        AlertMessage = "alert-success";
        //    } else
        //    {
        //        MessageStatusSearch = $"Không có sản phẩm phù hợp với kết quả tìm kiếm với từ khóa '{searchString}'";
        //        AlertMessage = "alert-danger";
        //    }

        //    ViewData["MessageSearch"] = MessageStatusSearch;
        //    //ViewBag.ProductSearch = products;
        //    ViewBag.Alert = AlertMessage;
        //    return View(products);
        //}

        [HttpGet]
        //[Route("tim-kiem-va-loc/")]
        public ActionResult Search(string? searchString, string? searchHistory, string? priceSortOrder, List<string>? brand, List<string>? gender, List<string>? capacity, List<string>? original)
        {
            SetValueViewBag();
            var stringSearchHistory = "";
            //var searchstring = Request.RouteValues["searchString"] as string;
            var products = _context.Products.ToList();
            if (searchString != null && searchString.Any())
            {
                products = _context.Products.Where(p => p.Product_Name.Contains(searchString)).ToList();
            }

            ViewBag.Search = searchString;

            if (searchHistory != null && searchHistory.Any())
            {
                products = _context.Products.Where(p => p.Product_Name.Contains(searchHistory)).ToList();
                stringSearchHistory = Request.Query["searchHistory"].ToString();
                ViewBag.StringSearchHistory = stringSearchHistory;
            }



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


            if (products != null && products.Any())
            {
                MessageStatusSearch = $"Danh sách sản phẩm sau khi tìm kiếm và lọc sản phẩm";
                AlertMessage = "alert-success";
            }
            else
            {
                MessageStatusSearch = $"Không có sản phẩm phù hợp với kết quả tìm kiếm và lọc sản phẩm";
                AlertMessage = "alert-danger";
            }

            ViewData["MessageSearch"] = MessageStatusSearch;
            ViewBag.ProductSearch = products;
            ViewBag.Alert = AlertMessage;
            return View(products);
        }






    }
}
