using QuizzAppFilRouge.Data.Entities;

namespace QuizzAppFilRouge.Models.AnswerViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsARightAnswer { get; set; }

        public Question Question { get; set; }
    }
}
