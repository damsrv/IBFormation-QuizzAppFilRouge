using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Data;

namespace QuizzAppFilRouge.Domain
{
    public interface IResponseRepository
    {

        Task AddResponse(Answer choosenAnswer, int quizzId, int questionId, string userId);
    }


    public class DbResponseRepository : IResponseRepository
    {
        private readonly ApplicationDbContext context;


        public DbResponseRepository(ApplicationDbContext context)
        {

            this.context = context;


        }


        public async Task AddResponse(Answer choosenAnswer, int quizzId, int questionId, string userId)
        {
            
        }
    }
}
