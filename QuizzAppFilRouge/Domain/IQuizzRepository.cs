using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;

namespace QuizzAppFilRouge.Domain
{
    public interface IQuizzRepository
    {

        Task<IEnumerable<Quizz>> GetAll();

        Task<Quizz> Details(int? id);

        Task<Quizz> GetQuizzById(int? id);

        Task Edit(Quizz quizz);

        Task<bool> QuizzExists(int id);

        Task Delete(int id);

        ApplicationDbContext returnContext ();

        //Task<UserInfo> GetUserInfoById(string identityUserId);
    }


    public class DbQuizzRepository : IQuizzRepository
    {

        private readonly ApplicationDbContext context;

        // Controller
        public DbQuizzRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Quizz>> GetAll()
        {

            var quizzs = await context.Quizzes
                .Include(quizz => quizz.Passages)// Le include permet de faire des Jointures
                .Include(user => user.QuizzCreator)
                .ToListAsync();

            //foreach (var quizz in quizzs)
            //{
                
            //    var passage = quizz.Passages.ToList();

            //    var identityUserId = passage[0].IdentityUserId;

            //    var userInfo = await GetUserInfoById(identityUserId);


            //}

            //var userInfo = GetUserInfoById(quizz)

            //var quizzs = await context.Quizzes.ToListAsync();
            return quizzs;
        }

        // TODO : Method GetAllByRecruiter (for recruiter only cos admin can see everything)

        public async Task<Quizz> Details(int? id)
        {

            var quizz = await context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
            
            return quizz;

        }

        public async Task<Quizz> GetQuizzById(int? id)
        {
            // Permet de récupéré toute les infos d'un QUizz
            var quizz = await context.Quizzes
            .Include(quizz => quizz.Passages) // Le include permet de faire des Jointures
            .Include(question => question.Questions)
            .Include(response => response.Responses)
            .Include(user => user.QuizzCreator)
            .FirstOrDefaultAsync(quizz => quizz.Id == id);


            //var quizz = context.Quizzes
            //    .FirstOrDefaultAsync(quizz => quizz.Id == id);

            return quizz;

        }

        public async Task Edit(Quizz quizz)
        {
            context.Update(quizz);
            context.SaveChanges();
        }


        public async Task<bool> QuizzExists(int id)
        {
            var exist = await context.Quizzes.AnyAsync(e => e.Id == id);
            
            return exist;
        }

        public async Task Delete(int id)
        {

            var quizz = await context.Quizzes.FindAsync(id);

            if (quizz != null)
            {
                context.Quizzes.Remove(quizz);
            }

            await context.SaveChangesAsync();
        }

        public ApplicationDbContext returnContext()
        {
            return context;
        }

    //    public async Task<UserInfo> GetUserInfoById(string identityUserId)
    //    {
    //        var userInfos = await context.UserInfos
    //            .FirstOrDefaultAsync(userInfos => userInfos.IdentityUser.Id == identityUserId);

    //        return userInfos;
    //    }
    }


}

