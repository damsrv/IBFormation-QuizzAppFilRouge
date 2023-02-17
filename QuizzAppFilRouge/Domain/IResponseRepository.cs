using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Data;
using Microsoft.EntityFrameworkCore;

namespace QuizzAppFilRouge.Domain
{
    public interface IResponseRepository
    {
        Task AddResponse(Response applicantResponse, int quizzId, int questionId);
        Task AddResponseForFreeQuestions(Response applicantResponse, int quizzId, int questionId);

    }


    public class DbResponseRepository : IResponseRepository
    {
        private readonly ApplicationDbContext context;


        public DbResponseRepository(ApplicationDbContext context)
        {

            this.context = context;


        }


        public async Task AddResponse
        (
            Response applicantResponse,
            int quizzId,
            int questionId)
        {
            // Select la bonne ligne en base
            var answer = await context.Responses
                .Where(response => response.QuizzId == quizzId
                        && response.QuestionId == questionId)
                .FirstAsync();

            // modifie le contenu de l'objet
            answer.Content = applicantResponse.Content;
            answer.IsCorrect = applicantResponse.IsCorrect;
            answer.Comment = applicantResponse.Comment;
            // save les modifications
            await context.SaveChangesAsync();
        }

        public async Task AddResponseForFreeQuestions
        (
            Response applicantResponse, 
            int quizzId, 
            int questionId
        )
        {
            // Select la bonne ligne en base
            var answer = await context.Responses
                .Where(response => response.QuizzId == quizzId
                        && response.QuestionId == questionId)
                .FirstAsync();

            // modifie le contenu de l'objet
            answer.Content = applicantResponse.Content;
            //answer.IsCorrect = applicantResponse.IsCorrect;
            answer.Comment = applicantResponse.Comment;
            // save les modifications
            await context.SaveChangesAsync();
        }
    }
}
