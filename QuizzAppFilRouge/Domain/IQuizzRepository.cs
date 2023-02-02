using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;

namespace QuizzAppFilRouge.Domain
{
    public interface IQuizzRepository
    {

        IEnumerable<Quizz> GetAll();

        Quizz Details(int? id);

        Quizz GetQuizzById(int? id);

        void Edit(Quizz quizz);

        bool QuizzExists(int id);

    }


    public class DbQuizzRepository : IQuizzRepository
    {

        private readonly ApplicationDbContext context;

        // Controller
        public DbQuizzRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Quizz> GetAll()
        {
            var quizzs = context.Quizzes.ToList();
            return quizzs;
        }

        // TODO : Method GetAllByRecruiter

        public Quizz Details(int? id)
        {

            var quizz = context.Quizzes
                .FirstOrDefault(m => m.Id == id);
            
            return quizz;

        }

        public Quizz GetQuizzById(int? id)
        {

            var quizz = context.Quizzes
            .Select(m => m)
            .Where(quizz => quizz.Id == id)
            //.Include(quizz => quizz.Passages) // Le include permet de faire des Jointures
            //.Include(question => question.Questions)
            .ToList();


            return quizz[0];

        }

        public void Edit(Quizz quizz)
        {
            context.Update(quizz);
            context.SaveChanges();
        }


        public bool QuizzExists(int id)
        {
            return (context.Quizzes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }


}

