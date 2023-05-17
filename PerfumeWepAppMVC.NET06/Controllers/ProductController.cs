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

            var releaseYearProduct = _context.Products.Select(p => p.Product_ReleaseYear).Distinct().ToList();
            ViewBag.releaseYearProduct = releaseYearProduct;

            var concentrationProduct = _context.ProductSpecs.Select(p => p.Concentration).Distinct().ToList();
            ViewBag.ConcentrationProduct = concentrationProduct;
        }

        public IActionResult Index()
        {
            SetValueViewBag();
            var listAllProduct = _context.Products.ToList();
            //TempData["products"] = listAllProduct;
            return View(listAllProduct);
        }

        public string MessageStatus { get; set; }

        [HttpGet]
        public IActionResult Filter(string? priceSortOrder, List<string>? brand, List<string>? gender, List<string>? capacity, List<string>? original)
        {
            SetValueViewBag();
            var products = _context.Products.ToList();

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
            
            if (brand != null && brand.Any())
            {
                products = products.Where(p => brand.Contains(p.Category_ID)).ToList();
            }

            if (gender != null && gender.Any())
            {
                products = products.Where(p => gender.Contains(p.Product_Gender)).ToList();
            }

            if (capacity != null && capacity.Any())
            {
                products = products.Where(p => capacity.Contains(p.Product_Volume)).ToList();
            }

            if (original != null && original.Any())
            {
                products = products.Where(p => original.Contains(p.Product_Origin)).ToList();
            }

            if (products != null && products.Any())
            {
                MessageStatus = "Danh sách kết quả sau khi lọc";
                ViewBag.Message = MessageStatus;
            } else
            {
                MessageStatus = "Không có sản phẩm nào phụ hợp với kết quả lọc của bạn";
                ViewBag.Message = MessageStatus;
            }
            return View(products);
        }

        [HttpGet]
        [Route("chi-tiet-san-pham/{id?}")]
        public ActionResult Detail(string? id)
        {
            var product = _context.Products.Where(p => p.Product_ID == id).FirstOrDefault();
            return View(product);
        }
    }
}
