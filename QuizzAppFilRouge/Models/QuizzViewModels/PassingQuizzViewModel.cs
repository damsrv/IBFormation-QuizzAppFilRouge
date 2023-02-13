using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Models.QuizzViewModels;
using QuizzAppFilRouge.Models.QuestionViewModels;
using QuizzAppFilRouge.Models.AnswerViewModels;


namespace QuizzAppFilRouge.Models.QuizzViewModels
{
    public class PassingQuizzViewModel
    {

        public QuizzViewModel QuizzViewModel { get; set; }

        public QuestionViewModel QuestionViewModel { get; set; }

        public List<AnswerViewModel> AnswerViewModel { get; set; }

    }
}
