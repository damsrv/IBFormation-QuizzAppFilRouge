using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuizzAppFilRouge.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuizzAppFilRouge.Data.Entities;

namespace QuizzAppFilRouge.Models.AnswerViewModels
{
    public class AnswerViewModel
    {
        [Required(ErrorMessage = "Veuillez entrer un ID")]
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsARightAnswer { get; set; }

        public Question Question { get; set; }

        public bool IsChecked { get; set; } = false;

    }


}

