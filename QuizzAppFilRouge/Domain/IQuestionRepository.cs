using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace QuizzAppFilRouge.Domain
{
    public interface IQuestionRepository
    {

        Task<List<Question>> GetAllQuestions();


    }



    public class DbQuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext context;


        public DbQuestionRepository(ApplicationDbContext context) 
        {
            this.context = context;

        }

        public async Task<List<Question>> GetAllQuestions()
        {
            var questionsList = await context.Questions
                .Include(answer => answer.Answers)
                .ToListAsync();

            return questionsList;
        }
    }
}
