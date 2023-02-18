using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Data;
using Microsoft.EntityFrameworkCore;

namespace QuizzAppFilRouge.Domain
{
    public interface IResponseRepository
    {
        Task AddResponse(Response applicantResponse, int quizzId, int questionId);
        Task AddResponseForFreeQuestions(Response applicantResponse, int quizzId, int questionId);

        Task<List<Response>> getQuizzResponses(int quizzId);

        Task<List<Response>> GetFreeQuestionResponses(int quizzId);

        Task AddCorrectionForFreeQuestion(Response correctedResponse);



    }


    public class DbResponseRepository : IResponseRepository
    {
        private readonly ApplicationDbContext context;


        public DbResponseRepository(ApplicationDbContext context)
        {

            this.context = context;


        }

        public async Task AddCorrectionForFreeQuestion(Response correctedResponse)
        {

            // Select la bonne ligne en base
            var response = await context.Responses
                .Where(response => response.QuizzId == correctedResponse.QuizzId
                        && response.QuestionId == correctedResponse.QuestionId)
                .FirstAsync();

            // Ajoute la correction à la question libre 
            response.IsCorrect = correctedResponse.IsCorrect;

            // save les modifications
            await context.SaveChangesAsync();
        }

        public async Task AddResponse
        (
            Response applicantResponse,
            int quizzId,
            int questionId)
        {
            // Select la bonne ligne en base
            var response = await context.Responses
                .Where(response => response.QuizzId == quizzId
                        && response.QuestionId == questionId)
                .FirstAsync();

            // modifie le contenu de l'objet
            response.Content = applicantResponse.Content;
            response.IsCorrect = applicantResponse.IsCorrect;
            response.Comment = applicantResponse.Comment;
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
            var response = await context.Responses
                .Where(response => response.QuizzId == quizzId
                        && response.QuestionId == questionId)
                .FirstAsync();

            // modifie le contenu de l'objet
            response.Content = applicantResponse.Content;
            //answer.IsCorrect = applicantResponse.IsCorrect;
            response.Comment = applicantResponse.Comment;
            // save les modifications
            await context.SaveChangesAsync();
        }

        public async Task<List<Response>> GetFreeQuestionResponses(int quizzId)
        {
            var quizzResponses = await context.Responses
                .Include(question => question.Question)
                .Where(response => response.QuizzId == quizzId && response.Question.QuestionType == QuestionTypeEnum.FreeQuestion)
                .OrderBy(response => response.QuestionId)
                .ToListAsync();

            return quizzResponses;
        }

        public async Task<List<Response>> getQuizzResponses(int quizzId)
        {
            var quizzResponses = await context.Responses
                .Include(question => question.Question)
                .Where(response => response.QuizzId == quizzId)
                .ToListAsync();

            return quizzResponses;
        }
    }
}
