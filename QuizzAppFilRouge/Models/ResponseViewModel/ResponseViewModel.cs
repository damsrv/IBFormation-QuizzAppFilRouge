using QuizzAppFilRouge.Data.Entities;

namespace QuizzAppFilRouge.Models.ResponseViewModel
{
    public class ResponseViewModel
    {

        public int Id { get; set; }

        public string? Content { get; set; } // VARCHAR MAX

        public ApplicationUser ApplicationUser { get; set; }

        public Question Question { get; set; }


        public Quizz Quizz { get; set; }

        public int QuizzId { get; set; }

        public int QuestionId { get; set; }


        public bool IsCorrect { get; set; }

        public bool IsTrue { get; set; }

        public bool IsFalse { get; set; }

        public bool IsChecked { get; set; } = false;

        public string? Comment { get; set; }

    }
}
