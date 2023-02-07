using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuizzAppFilRouge.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzAppFilRouge.Models.QuizzViewModel
{
    public class QuizzViewModel
    {
        [Required(ErrorMessage = "Veuillez entrer un ID")]
        public int Id { get; set; }

        public double? Notation { get; set; }

        [StringLength(100)]
        public string? ValidationCode { get; set; }

        [ForeignKey("ApplicationUser")]
        public virtual ApplicationUser QuizzCreator { get; set; }

        public int TotalQuestionNumber { get; set; } 

        public int FreeQuestionPercentage { get; set; }   

        public QuizzLevelEnum QuizzLevel { get; set; }

        public ICollection<Passage> Passages { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<Response> Responses { get; set; }

        public List<SelectListItem> HandledByMeCandidates{ get; set; }

        public string selectedCandidateId { get; set; }

        public DateTime PassageDate { get; set; }




    }
}
