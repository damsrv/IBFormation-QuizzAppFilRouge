using Microsoft.EntityFrameworkCore;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;

namespace QuizzAppFilRouge.Domain
{
    public interface IAnswerRepository
    {

        Task<List<Answer>> getAnswerByQuestionId(int questionId);

        //Task<Answer> getAllAnswers();
    }



    public class DbAnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext context;

        public DbAnswerRepository(ApplicationDbContext context)
        {
            this.context = context;

        }
        public async Task<List<Answer>> getAnswerByQuestionId(int questionId)
        {
            var answers = await context.Answers
                .Where(answer => answer.Id == questionId)
                .ToListAsync(); 

            return answers;
        }
    }

}
