using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace QuizzAppFilRouge.Data.Entities
{
    [PrimaryKey (nameof(QuizzId), nameof(IdentityUserId))]
    public class Passage
    {
        // Les deux clés composant la clé composite
        public int QuizzId { get; set; } 
        public string IdentityUserId { get; set; }

        // Date de passage du Quizz
        public DateTime PassageDate { get; set; }

        public IdentityUser IdentityUser { get; set; }

        public Quizz Quizz { get; set; }

    }
}
