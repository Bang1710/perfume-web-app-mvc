using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
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