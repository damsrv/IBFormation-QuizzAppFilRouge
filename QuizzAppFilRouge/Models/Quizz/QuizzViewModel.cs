using Microsoft.AspNetCore.Identity;
using QuizzAppFilRouge.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace QuizzAppFilRouge.Models.QuizzViewModel
{
    public class QuizzViewModel
    {
        [Required(ErrorMessage = "Veuillez entrer un ID")]
        public int Id { get; set; }

        public double? Notation { get; set; }

        [StringLength(100)]
        public string? ValidationCode { get; set; }

        public IdentityUser QuizzCreator { get; set; }

        public int NombreQuestions { get; set; } 

        public int NombreQuestionLibres { get; set; }   

        public QuizzLevelEnum QuizzLevel { get; set; }





    }
}
