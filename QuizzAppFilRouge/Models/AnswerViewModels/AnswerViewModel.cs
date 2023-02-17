using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuizzAppFilRouge.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Models.UserViewModels;

namespace QuizzAppFilRouge.Models.AnswerViewModels
{
    public class AnswerViewModel
    {
        [Required(ErrorMessage = "Veuillez entrer un ID")]
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsARightAnswer { get; set; }

        public Question Question { get; set; }

        // Validation ne marche pas à voir plus tard
        [CheckBoxRequired(ErrorMessage = "Veuillez cocher au moins une réponse")]
        public bool IsChecked { get; set; }

        public string FreeQuestionAnswer { get; set; }




    }

    public class CheckBoxRequiredAttribute : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //get the entered value
            
            var answer = (AnswerViewModel)validationContext.ObjectInstance;
            //Check whether the IsAccepted is selected or not.
            if (answer.IsChecked == false)
            {
                //if not checked the checkbox, return the error message.
                return new ValidationResult(ErrorMessage == null ? "Please checked the checkbox" : ErrorMessage);
            }
            return ValidationResult.Success;
        }

    }
}

