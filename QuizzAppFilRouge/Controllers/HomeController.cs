using Microsoft.AspNetCore.Mvc;
using QuizzAppFilRouge.Models;
using System.Diagnostics;
using System.Security.Claims;
//using ChartJs.Blazor.ChartJS.BarChart;
//using ChartJs.Blazor.ChartJS.Common.Axes;
//using ChartJs.Blazor.ChartJS.Common.Axes.Ticks;
//using ChartJs.Blazor.ChartJS.Common.Properties;
//using ChartJs.Blazor.ChartJS.LineChart;
//using ChartJs.Blazor.ChartJS.LineChart.Axes;
//using ChartJs.Blazor.ChartJS.LineChart.Axes.Ticks;
//using ChartJs.Blazor.ChartJS.LineChart.Dataset;
//using ChartJs.Blazor.ChartJS.LineChart.Options;

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

        //public IActionResult MyChart()
        //{
        //    // Récupérez les données pour votre graphique ici
        //    var data = new int[] { 10, 20, 30, 40, 50 };

        //    // Créez l'objet Chart.js et configurez-le
        //    var chart = new Chart(new BarChart("myChart", new BarDataset(data)
        //    {
        //        Label = "Données de test"
        //    }));

        //    // Renvoyez la vue avec le graphique
        //    return View(chart);
        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}