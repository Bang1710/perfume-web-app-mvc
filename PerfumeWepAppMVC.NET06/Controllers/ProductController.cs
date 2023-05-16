using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeWebApp.NET06.Data;

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

        [HttpGet]
        public IActionResult Index()
        {
            SetValueViewBag();
            var listAllProduct = _context.Products.ToList();
            return View(listAllProduct);
        }

        [HttpGet]
        public IActionResult Filter(string priceSortOrder, List<string> brand, List<string> gender, List<string> capacity, List<string> original, List<string> concentration, List<string> YearRelease)
        {
            SetValueViewBag();
            var products = _context.Products.ToList();

            if (priceSortOrder != null)
            {
                switch (priceSortOrder)
                {
                    case "asc":
                        products = products.OrderBy(p => p.Product_Price).ToList();
                        break;
                    case "desc":
                        products = products.OrderByDescending(p => p.Product_Price).ToList();
                        break;
                    default:
                        products = products.OrderBy(p => p.Product_Price).ToList();
                        break;
                }
            }

            if (brand != null)
            {
                products = products.Where(p => brand.Contains(p.Category_ID)).ToList();
            }
            return View("Filter", products);
        }
    }
}
