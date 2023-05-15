using Microsoft.AspNetCore.Mvc;
using PerfumeWebApp.NET06.Data;

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

    }
}