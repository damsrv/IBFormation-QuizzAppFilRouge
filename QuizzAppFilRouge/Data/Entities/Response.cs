using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace QuizzAppFilRouge.Data.Entities
{
    public class Response
    {

        public int Id { get; set; }

        public string? Content { get; set; } // VARCHAR MAX

        public int QuestionId { get; set; }

        public string IdentityUserId { get; set; }

        public int QuizzId { get; set; }

        public Question Question { get; set; }

        public IdentityUser IdentityUser { get; set; }

        public Quizz Quizz { get; set; }


        


    }
}
