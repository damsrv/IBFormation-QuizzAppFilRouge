using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QuizzAppFilRouge.Models.Role
{
    public class CreateRoleViewModel
    {
        // Pas deboins car IdentityRole semble déja exister pour gérer la création des rols



        [StringLength(50)]
        [Required(ErrorMessage = "Veuillez entrer un Nom de role svp.")]
        public string Name { get; set; }

    }
}
