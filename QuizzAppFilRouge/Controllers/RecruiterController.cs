using Microsoft.AspNetCore.Mvc;

namespace QuizzAppFilRouge.Controllers
{
    public class RecruiterController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views\\RecruiterPage\\RecruiterPage.cshtml");

        }
    }
}
