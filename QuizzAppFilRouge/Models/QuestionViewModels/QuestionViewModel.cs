using QuizzAppFilRouge.Data.Entities;

namespace QuizzAppFilRouge.Models.QuestionViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        public string? Content { get; set; } // VARCHAR MAX

        public QuestionLevelEnum QuestionLevel { get; set; }

        public QuestionTypeEnum QuestionType { get; set; }

        public QuizzLangageEnum QuestionLangage { get; set; }


        public virtual ICollection<Quizz> Quizzes { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public Question ActualQuestion { get; set; }

        public int ActualQuestionNumber { get; set; }

    }
}
