using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace QuizzAppFilRouge.Data.Entities
{
    [PrimaryKey (nameof(QuizzId), nameof(ApplicationUserId))]
    public class Passage
    {
        // Les deux clés composant la clé composite
        public int QuizzId { get; set; } 
        public string ApplicationUserId { get; set; }

        // Date de passage du Quizz
        public DateTime PassageDate { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Quizz Quizz { get; set; }

    }
}
