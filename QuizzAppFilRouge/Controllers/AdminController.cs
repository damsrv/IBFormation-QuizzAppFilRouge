using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizzAppFilRouge.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
        public IActionResult Index()
        {
            
            return View("~/Views\\AdminPage\\AdminPage.cshtml");
        }
    }
}
