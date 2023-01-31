namespace QuizzAppFilRouge.Data.Entities
{
    public class Question
    {

        public int Id { get; set; }

        public string? Content { get; set; } // VARCHAR MAX

        public QuestionLevelEnum QuestionLevel { get; set; }

        public QuestionTypeEnum QuestionType { get; set; }

        public virtual ICollection<Quizz> Quizzes { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }


    }

}
