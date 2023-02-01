using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QuizzAppFilRouge.Models
{
    public class CreateUserViewModel
    {

        [StringLength(50)]
        [Required(ErrorMessage = "Veuillez entrer votre Nom")]
        public string FirsName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Veuillez entrer votre Prenom")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Veuillez entrer votre Email")]
        public string Email { get; set; }
        
        // Mettre une string et non un IdentityRole
        // car ce qui est donné par le ViewModel est une string
        public string RoleName { get; set; }

    }
}
