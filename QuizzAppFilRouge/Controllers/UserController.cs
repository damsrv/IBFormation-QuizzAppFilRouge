using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Models;

namespace QuizzAppFilRouge.Controllers
{
    public class UserController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;

        private readonly ApplicationDbContext context;


        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }
    
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUserList()
        {
            var user = userManager.Users.ToList();
            return View(user);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        /**
        * Méthode Post de création des Users
        * Va créer un IdentityUser ( qui va peupler la table AspNetUsers) et un UserInfo ( qui va peupler la table UserInfos) 
        * Se sert du CreateUserViewModel (viewmodel lié au formulaire) pour obtenir les données du User à créer.
        **/
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel viewModelUser)
        {
            if (ModelState.IsValid)
            {
                // Utilisation de l'objet UserManager auquel on passe un objet de type IdentityUser.
                // Appel de la fonction CreateAsync qui va tenter d'enregistrer le nouvel user dans la base (table ASPNETUSERS)
                var identityUser = createIdentityUser(viewModelUser);
                IdentityResult identityResult = await userManager.CreateAsync(identityUser);

                // Si email déja utilisé dans base (la requete à échouée)
                if (identityResult.Succeeded == false) 
                {
                    ModelState.AddModelError("Email", "L'email est déja utilisé ");
                    return View();
                }
                // Si pas déja email dans la base (la requete à réussie)
                else
                {
                    // Créer le User dans la table UserInfo
                    createUserInfo(identityUser, viewModelUser);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }    
        }

        /**
         * Créer l'objet IdentityUser qui va aller peupler la table AspNetUser  
         */
        public IdentityUser createIdentityUser(CreateUserViewModel viewModelUser)
        {
            IdentityUser identityUser = new IdentityUser();

            // Création du User dans table AspNetUsers
            identityUser.Email= viewModelUser.Email;
            identityUser.UserName = viewModelUser.Email;
            identityUser.EmailConfirmed = true;
            identityUser.NormalizedEmail = identityUser.Email.ToUpper();

            return identityUser;

        }

        /**
         * Créer l'objet UserInfo qui va aller peupler la table UserInfo
         */
        public void createUserInfo(IdentityUser identityUser, CreateUserViewModel viewModelUser)
        {
            
            UserInfo userInfo = new UserInfo
            {
                FirstName = viewModelUser.FirsName,
                LastName = viewModelUser.FirsName,
                IdentityUser = identityUser, // correspont à la foreign key qui lie AspNetUsers et UserInfos
            };

            // Ajout infos dans table UserInfos
            context.UserInfos.Add(userInfo);
            context.SaveChanges();

            
        }

    }

}
