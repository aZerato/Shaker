using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.domain.Posts;
using shaker.Models;

namespace shaker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostsDomain _postsDomain;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IPostsDomain postsDomain,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _postsDomain = postsDomain;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}
