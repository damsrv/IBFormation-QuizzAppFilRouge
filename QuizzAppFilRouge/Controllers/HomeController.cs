using Microsoft.AspNetCore.Mvc;
using QuizzAppFilRouge.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace QuizzAppFilRouge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        
        public IActionResult Index()
        {
            //ViewData["PageName"] = "accueil";
            return View(); 
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}