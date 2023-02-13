using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Models.UserViewModels;
using System.Data;
using System.Dynamic;

namespace QuizzAppFilRouge.Controllers
{
    [Authorize]
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


//////////////////////////////////////////////////////////////////////////////////////
//// GETUSERLIST FUNCTIONS 
//////////////////////////////////////////////////////////////////////////////////////


        public IActionResult GetUserList()
        {
            var listUsersViewModel = new List<UserViewModel>();

            var listApplicationUsers = context.ApplicationUsers.ToList();


            foreach (var user in listApplicationUsers)
            {
                
                listUsersViewModel.Add(createUserViewModel(user));    
            }

            return View(listUsersViewModel);
        }


        //////////////////////////////////////////////////////////////////////////////////////
        //// CREATE USER GET AND POST FUNCTIONS OK
        //////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        /**
        * Méthode Post de création des Users
        * Va créer un ApplicationUser ( qui va peupler la table AspNetUsers)
        * Se sert du CreateUserViewModel (viewmodel lié au formulaire) pour obtenir les données du User à créer.
        **/
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel viewModelUser)
        {

            var applicationUser = createAppUser(viewModelUser);

            if (ModelState.IsValid)
            {
                // Utilisation de l'objet UserManager auquel on passe un objet de type IdentityUser.
                // Appel de la fonction CreateAsync qui va tenter d'enregistrer le nouvel user dans la base (table ASPNETUSERS)
                
                

                IdentityResult identityResult = await userManager.CreateAsync(applicationUser);

                // Si email déja utilisé dans base (la requete à échouée)
                if (identityResult.Succeeded == false)
                {
                    ModelState.AddModelError("Email", "L'email est déja utilisé ");
                    return View();
                }
                // Si pas déja email dans la base (la requete à réussie)
                else
                {
                    // TODO : Créer le role correspondant
                    // TODO : Réfléchir au cas de connexion hors admin ? Faut t'il que les recruteurs puissent s'identifier ou pas ? 

                    return RedirectToAction("Index");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View();
            }
        }


//////////////////////////////////////////////////////////////////////////////////////
//// OTHER FUNCTIONS 
//////////////////////////////////////////////////////////////////////////////////////
        
        /**
         * Créer l'objet ApplicationUser qui va aller peupler la table AspNetUser  
         */
        public ApplicationUser createAppUser(UserViewModel viewModelUser)
        {
            ApplicationUser applicationUser = new ApplicationUser();

            // Création du User dans table AspNetUsers

            applicationUser.Id = Guid.NewGuid().ToString();
            applicationUser.Email = viewModelUser.Email;
            applicationUser.UserName = viewModelUser.Email;
            applicationUser.FirstName = viewModelUser.FirstName;
            applicationUser.LastName = viewModelUser.LastName;
            applicationUser.BirthDate = viewModelUser.BirthDate;
            applicationUser.EmailConfirmed = true;
            applicationUser.NormalizedEmail = viewModelUser.Email.ToUpper();
            applicationUser.PhoneNumberConfirmed = false;
            applicationUser.TwoFactorEnabled = false; 
            applicationUser.PasswordHash = viewModelUser.Password; // Problème créer le mot de passe en dur dans la base.

            // Pas besoin de créer de Password pour les Candidat (car vont se connecter indirectement)
            // A faire ROLE
            return applicationUser;

        }


        /**
        * Créer les Objets UserViewModel qui sont passées à la vue  
        */
        public UserViewModel createUserViewModel(ApplicationUser applicationUser)
        {
            UserViewModel userViewModel = new UserViewModel();

            userViewModel.Id = applicationUser.Id;

            userViewModel.FirstName = applicationUser.FirstName;
            userViewModel.LastName = applicationUser.LastName;
            userViewModel.BirthDate = applicationUser.BirthDate;
            userViewModel.Email = applicationUser.Email;

            return userViewModel;

        }

        public void createUserRole  ()
        {


        }


    }

}
