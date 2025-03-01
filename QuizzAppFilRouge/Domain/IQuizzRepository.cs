﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Models.QuizzViewModels;

namespace QuizzAppFilRouge.Domain
{
    public interface IQuizzRepository
    {

        Task<List<Quizz>> GetAll();

        Task<List<Quizz>> GetQuizzCreatedByUser(string idUser);

        Task<Quizz> Details(int? id);

        Task<Quizz> GetQuizzById(int? id);

        Task Edit(Quizz quizz);

        Task<bool> QuizzExists(int id);

        Task Delete(int id);

        ApplicationDbContext returnContext ();

        Task Create(Quizz quizz);


        Task<int> GetLastQuizzId();

        Task<bool> CheckValidationCode(QuizzViewModel quizzViewModel);

        Task<string> GetValidationCode(int quizzId);

        Task AddNotation(double notation, int quizzId);

        Task<string> GetPassageDate(int quizzid);
    }


    public class DbQuizzRepository : IQuizzRepository
    {

        private readonly ApplicationDbContext context;
        private readonly IUserRepository userRepository;


        // Controller
        public DbQuizzRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            this.context = context;
            this.userRepository = userRepository;
        }

        public async Task<List<Quizz>> GetAll()
        {

            var quizzs = await context.Quizzes
                .Include(quizz => quizz.Passages)// Le include permet de faire des Jointures
                    .ThenInclude(passage => passage.ApplicationUser)
                .Include(user => user.QuizzCreator)
                .ToListAsync();

            return quizzs;
        }

        public async Task<List<Quizz>> GetQuizzCreatedByUser(string idUser)
        {

            var quizzs = await context.Quizzes
                .Where(quizz => quizz.QuizzCreator.Id == idUser)
                .Include(quizz => quizz.Passages)// Le include permet de faire des Jointures
                    .ThenInclude(passage => passage.ApplicationUser)
                .Include(user => user.QuizzCreator)
                .Include(quizz => quizz.Questions)
                .ToListAsync();

            return quizzs;
        }


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
                    .ThenInclude(passage => passage.ApplicationUser)
                .Include(question => question.Questions)
                    .ThenInclude(question => question.Answers)
                .Include(response => response.Responses)
                .Include(user => user.QuizzCreator)
                .FirstOrDefaultAsync(quizz => quizz.Id == id);
                

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

        public async Task Create(Quizz newQuizz)
        {
           // Solution avec Attach à permis de régler le bug dans lequel le programme
           // essayait de créer une nouvelle entité User (ce qui engendrais un conflit de clé primaire unique)
           // au lieu de simplement inscrire la valeur dans la colonne QuizzCreatorId FK qui pointe vers UserId
           // Voir mail de Julien pour explication
            context.Attach(newQuizz.QuizzCreator);

            await context.Quizzes
                .AddAsync(newQuizz);

            context.SaveChanges();


        }

        public async Task<int> GetLastQuizzId()
        {
            var id = await context.Quizzes
                .Select(quizz => quizz.Id)
                .LastAsync();

            return id;
        }

        public async Task<bool> CheckValidationCode(QuizzViewModel quizzViewModel)
        {

            return quizzViewModel.ValidationCode == quizzViewModel.EnteredValidationCode ? true : false;

        }

        public async Task<string> GetValidationCode( int quizzId )
        {
            var quizz = await context.Quizzes
                .Select(quizz => quizz)
                .Where(quizz => quizz.Id == quizzId)
                .FirstAsync();

            return quizz.ValidationCode;
        }

        public async Task AddNotation(double notation, int quizzId)
        {
            // Bonne maniere de modifier seulement une colonne dans une table
            var quizz = await context.Quizzes
                .FirstOrDefaultAsync(quizz => quizz.Id == quizzId);

            quizz.Notation = notation;
            
            await context.SaveChangesAsync();
        }

        public async Task<string> GetPassageDate(int quizzid)
        {
            var passage = await context.Passages
                .Where(passage => passage.QuizzId == quizzid)
                .Select(passage => passage.PassageDate.ToString())
                .FirstOrDefaultAsync();

            return passage;
        }
    }


}

