using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using QuizzAppFilRouge.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzAppFilRouge.Data.Entities
{
    [Authorize]
    public class Quizz
    {
        public int Id { get; set; }

        public double? Notation { get; set; }

        public string? ValidationCode { get; set; }

        public ApplicationUser QuizzCreator { get; set; }

        public QuizzLevelEnum QuizzLevel { get; set; }

        public virtual Passage Passages { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Response> Responses { get; set; }

    }
}
