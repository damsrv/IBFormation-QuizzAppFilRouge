using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace QuizzAppFilRouge.Data.Entities
{
    [Authorize]
    public class Quizz
    {
        
        
        public int Id { get; set; }

        public double? Notation { get; set; }

        public string? ValidationCode { get; set; }

        public virtual ICollection<Passage> Passages { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

}
}
