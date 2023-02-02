using QuizzAppFilRouge.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace QuizzAppFilRouge.Models.QuizzViewModel
{
    public class QuizzViewModel
    {
        [Required(ErrorMessage = "Veuillez entrer un ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Veuillez entrer votre Prenom")]
        public double? Notation { get; set; }

        [StringLength(100)]
        public string? ValidationCode { get; set; }

    }
}
