using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizzAppFilRouge.Models.Role;

namespace QuizzAppFilRouge.Controllers
{
    // Impossible d'implémenter une surcouche avec un Repo pour les roles sur cette classe car
    // l'injection de dépendance ne fonctionne pas sur une classe que le constructeur pour le type d'objet RoleManager
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {

        private RoleManager<IdentityRole> roleManager;


        public RoleController(RoleManager<IdentityRole> roleManager) 
        {
            this.roleManager = roleManager;
        
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetRoleList()
        {

            var roles = roleManager.Roles.ToList();
            return View(roles);
        }


        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {

            IdentityRole role = new IdentityRole();

            if(ModelState.IsValid)
            {
                role.Name = model.Name;

                await roleManager.CreateAsync(role);

                ViewBag.SuccessMessage = "testest ";

                //ModelState.Clear();


                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {
            return View();
        }

    }
}
