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
            return View();
        }

        public Product getProductById(string id)
        {
            return _context.Products.Where(p => p.Product_ID == id).FirstOrDefault();
        }

    }
}