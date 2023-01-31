using System.ComponentModel.DataAnnotations;

namespace QuizzAppFilRouge.Models.Role
{
    public class AddRoleViewModel
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Veuillez entrer un Nom de role svp.")]
        public string Name { get; set; }
    }
}
