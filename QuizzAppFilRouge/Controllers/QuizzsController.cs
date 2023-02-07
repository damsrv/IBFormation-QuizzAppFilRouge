﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Domain;
using QuizzAppFilRouge.Models.QuizzViewModel;

namespace QuizzAppFilRouge.Controllers
{

    public class QuizzsController : Controller
    {
        private readonly IQuizzRepository quizzsRepository;
        private readonly IUserRepository userRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly string actualLoggedUserId;


        // Constructor
        public QuizzsController
        (

            IQuizzRepository quizzsRepository,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IQuestionRepository questionRepository
        )
        {
            this.quizzsRepository = quizzsRepository;
            this.userRepository = userRepository;
            // Permet de récupérer l'id du user actuellement connecté
            var loggedUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.actualLoggedUserId = loggedUserId;
            this.questionRepository = questionRepository;
        }

        //OK
        public IActionResult Index()
        {

            return View();

        }



        //////////////////////////////////////////////////////////////////////////////////////
        //// GETALLQUIZZS AND GETALLQUIZZSBYID (A FAIRE) FUNCTIONS 
        //////////////////////////////////////////////////////////////////////////////////////

        //OK
        // GET: Quizzs
        public async Task<IActionResult> GetAllQuizzs()
        {

            var quizzes = await quizzsRepository.GetAll();

            var listQuizzViewModel = new List<QuizzViewModel>();

            foreach (var quizz in quizzes)
            {

                listQuizzViewModel.Add(new QuizzViewModel
                {
                    Id = quizz.Id,
                    Notation = quizz.Notation,
                    ValidationCode = quizz.ValidationCode,
                    QuizzLevel = quizz.QuizzLevel,
                    QuizzCreator = quizz.QuizzCreator,

                });

            }

            return View(listQuizzViewModel);

        }


        //////////////////////////////////////////////////////////////////////////////////////
        //// DETAILS FUNCTIONS OK
        //////////////////////////////////////////////////////////////////////////////////////

        // OK
        // GET: Quizzs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var quizz = await quizzsRepository.Details(id);

            if (quizz == null)
            {
                return NotFound();
            }

            var quizzViewModel = new QuizzViewModel
            {
                Id = quizz.Id,
                Notation = quizz.Notation,
                ValidationCode = quizz.ValidationCode,
                QuizzLevel = quizz.QuizzLevel,
                QuizzCreator = quizz.QuizzCreator,
            };

            return View(quizzViewModel);
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        ////// EDIT FUNCTIONS GET AND POST OK
        ////////////////////////////////////////////////////////////////////////////////////////

        //OK
        // GET: Quizzs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var quizz = await quizzsRepository.GetQuizzById(id);

            // Est ce normal d'avoir à transformer l'objet en Viewmodel
            // pour faire passer le modèle à la vue ??? 
            var quizzViewModel = new QuizzViewModel
            {
                Id = quizz.Id,
                Notation = quizz.Notation,
                ValidationCode = quizz.ValidationCode,
            };

            if (quizz == null)
            {
                return NotFound();
            }

            return View(quizzViewModel);

        }

        // OK
        // POST: Quizzs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuizzViewModel quizzViewModel)
        {

            // Est ce normal d'avoir à recaster le view model 
            // en object Quizz pour envoyer la modif vers la base
            // Y'a t'il une autre méthode ???
            var quizz = new Quizz
            {
                Id = quizzViewModel.Id,
                Notation = quizzViewModel.Notation,
                ValidationCode = quizzViewModel.ValidationCode,
            };

            if (ModelState.IsValid)
            {
                try
                {
                    quizzsRepository.Edit(quizz);

                }
                catch (DbUpdateConcurrencyException)
                {
                    var exist = await quizzsRepository.QuizzExists(quizz.Id);
                    if (!exist)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //var errors = ModelState.Select(x => x.Value.Errors)
            //           .Where(y => y.Count > 0)
            //           .ToList();
            return RedirectToAction(nameof(Index));
        }


        ////////////////////////////////////////////////////////////////////////////////////////
        ////// DELETE FUNCTIONS GET AND POST OK
        ////////////////////////////////////////////////////////////////////////////////////////

        // GET: Quizzs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var quizz = await quizzsRepository.GetQuizzById(id);

            if (quizz == null)
            {
                return NotFound();
            }


            var quizzViewModel = new QuizzViewModel
            {
                Id = quizz.Id,
                Notation = quizz.Notation,
                ValidationCode = quizz.ValidationCode,
                QuizzCreator = quizz.QuizzCreator,
                QuizzLevel = quizz.QuizzLevel,
                Passages = quizz.Passages,
                //UserInfos= quizz.UserInfos,  

            };

            return View(quizz);
        }

        // OK DELETE bien en cascade
        // Tout ce qui est contenue dans passage,response et question quizz
        // POST: Quizzs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (quizzsRepository.returnContext().Quizzes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Quizzes'  is null.");
            }

            await quizzsRepository.Delete(id);


            return RedirectToAction(nameof(Index));
        }



        ////////////////////////////////////////////////////////////////////////////////////////
        ////// CREATE FUNCTIONS GET AND POST
        ////////////////////////////////////////////////////////////////////////////////////////

        // GET: Quizzs/Create
        public async Task<IActionResult> Create()
        {


            // Récupérer les users que la personne connectée gère
            var userList = await userRepository.GetUserHandledById(actualLoggedUserId);

            var quizzViewModel = new QuizzViewModel();
            quizzViewModel.HandledByMeCandidates = new List<SelectListItem>();

            // Créer la select list HandledByMeCandidates qui va être envoyée à la vue
            foreach (var user in userList)
            {
                quizzViewModel.HandledByMeCandidates
                    .Add(new SelectListItem
                    {
                        Text = user.FirstName + " " + user.LastName + " " + user.Email,
                        Value = user.Id
                    });
            }



            return View(quizzViewModel);
        }


        // POST: Quizzs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizzViewModel quizzViewModel)
        {
            // Récuprère toute les questions
            var questionLists = await questionRepository.GetAllQuestions();

            // Créer une liste de questions de maniere aléatoire 
            var randowQuestionsFinalList = selectRandomQuestionForQuizz
            (
                questionLists,
                (int)quizzViewModel.QuizzLevel,
                quizzViewModel.TotalQuestionNumber,
                quizzViewModel.FreeQuestionPercentage
            );


            ApplicationUser userLogged = new ApplicationUser();
            userLogged.Id = actualLoggedUserId;

            // ETAPES
            // Actuellement on à une liste de questions avec les réponses correspondantes
            // Il faut peupler les autres tables liées à questions
            Quizz newQuizz = new Quizz
            {
                Notation = 0,
                ValidationCode = generateGuid(),
                QuizzLevel = quizzViewModel.QuizzLevel,
                QuizzCreator = userLogged,

            };

            //1 aller peupler quizz
            await quizzsRepository.Create(newQuizz);
            //2 récup id du quizz dernierement créé


            // PROBLEME LORS CREATION DU QUIZZ
            // IL ESSAYE DE CREER UNE ENTREE DANS TABLE USER
            // PROBABLEMENT MAL GERER RELATION ENTRE LES DEUX ENTITES QUIZZ ET USER







            // TODO : Function qui Créer des questions 
            // 1) créer object quizz

            //var quizz = new Quizz
            //{
            //    QuizzCreator = HandledCandidates;

            //};

            //if (ModelState.IsValid)
            //{
            //    _context.Add(quizz);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(quizz);
            return RedirectToAction(nameof(Index));
        }


////////////////////////////////////////////////////////////////////////////////////////
////// OTHER FUNCTIONS 
///////////////////////////////////////////////////////////////////////////////////////
        
        public string generateGuid () 
        {
            // création du guid
            var guid = Guid.NewGuid().ToString();
            return guid;
        }


        /**
         * Méthode qui va selectionner dans la liste complete des questions 
         * Le nombre de questions libre requis (forcément des questions du niveau du quizz (pas de selection random poru l'instant)
         * Le nombre de question qcm requis 
         * En fonction du niveau du quizz
         */
        public List<Question> selectRandomQuestionForQuizz
        (
            List<Question> questionList,
            int quizzLevel,
            double totalQuestionNumber,
            int freeQuestionPercentages
        )
        {
            // Transforme pourcentage free question en nombre (fct du nombre de question)
            double freeQuestionNumber = (totalQuestionNumber / 100) * freeQuestionPercentages;

            var randomFreeQuestionList = selectRandomFreeQuestion(quizzLevel, questionList, freeQuestionNumber, totalQuestionNumber);

 
            // OK JUSQUE ICI
            var randomQCMQuestionList = selectRandomQCMQuestion(quizzLevel, questionList, freeQuestionNumber, totalQuestionNumber);

            var randowQuestionsFinalList = randomFreeQuestionList
                .Concat(randomQCMQuestionList)
                .ToList();

            return randowQuestionsFinalList;

        }

        // var test = selectRandomFreeQuestion(quizzLevel, questionList, freeQuestionNumber, totalQuestionNumber);
        // 



////////////////////////////////////////////////////////////////////////////////////////
////// SELECT RANDOM FREE QUESTIONS FUNCTIONS 
///////////////////////////////////////////////////////////////////////////////////////
        /**
         * 
         * 
         */
        public List<Question> selectRandomFreeQuestion 
        (
            int quizzLevel,
            List<Question> questionList,
            double freeQuestionNumber,
            double totalQuestionNumber
        )
        {
            // Select en base les free questions ayant le niveau du quizz 
            var freeQuestionListByQuizzLevel = selectFreeQuestionByQuizzLevel(questionList, quizzLevel);

            // Dans toutes les questions selectionnée en base on choisit randomly
            // Des questions libre en fonction du nombre souhaité
            var freeQuestionRandomList = selectFreeQuestionRandomly(freeQuestionNumber, freeQuestionListByQuizzLevel);

            //selectQCMQuestionRandomly ()
            return freeQuestionRandomList;

        }

        public List<Question> selectFreeQuestionByQuizzLevel (List<Question> questionList, int quizzLevel)
        {
            // Selectionne dans la liste des free questions le nombre de free questions qui ont le niveau du quizz
            var freeQuestionListByLevel = questionList
                .Select(question => question)
                .Where(question => (int)question.QuestionType == (int)QuestionTypeEnum.FreeQuestion)
                .Where(question => (int)question.QuestionLevel == quizzLevel) // du niveau du quizz
                .ToList();


            return freeQuestionListByLevel;
        }

        public List<Question> selectFreeQuestionRandomly(double freeQuestionNumber, List<Question> freeQuestionListByQuizzLevel )
        {
            var random = new Random();
            List<Question> randomFreeQuestionList = new List<Question>();

            // Boucle pour selection randomly les free questions (du niveau du quizz)
            for (int i = 0; i < freeQuestionNumber; i++)
            {
                int randomNumber = random.Next(freeQuestionListByQuizzLevel.Count);
                randomFreeQuestionList.Add(freeQuestionListByQuizzLevel[randomNumber]);
                freeQuestionListByQuizzLevel.RemoveAt(randomNumber);
            }

            return randomFreeQuestionList;
        }


        ////////////////////////////////////////////////////////////////////////////////////////
        ////// SELECT QCM  QUESTIONS FUNCTIONS 
        ///////////////////////////////////////////////////////////////////////////////////////

        // obj :
        // avoir une liste avec un nombre de questions qcm en fonction 
        // du niveau du quizz - le nombre de question libre

        /**
        * 
        * 
        */
        public List<Question> selectRandomQCMQuestion
        (
            int quizzLevel,
            List<Question> questionList,
            double freeQuestionNumber,
            double totalQuestionNumber
        )
        {
        // https://localhost:7256/Quizzs/Create

            // calcul le nombre de question par niveau en fonction du niveau du quizz
            // Renvoi dictionnaire Dic<niveau, nombre de questions>
            var numberOfQuestionsByLevel = calculateQuestionNumberByLevel(totalQuestionNumber, quizzLevel);

            // Enleve un nombre (équivalent au nombre de questions libre dans le quizz)
            // de question dans le dictionnaire <niveau, nombre de questions>
            // dans la tranche du dictionnaire qui correspond au niveau du quizz
            numberOfQuestionsByLevel[quizzLevel] = numberOfQuestionsByLevel[quizzLevel] - freeQuestionNumber;
            
            // OK jusqu'ici
            
            // Select dans full list de question les QCM questions 
            var qcmQuestionOnlyList = selectQCMQuestionOnly(questionList);

            // Dans toutes les questions selectionnée en base on choisit randomly
            // Des questions libre en fonction du nombre souhaité
            // OK FONCTIONNE NICKEL
            var qcmQuestionRandomList = selectQCMQuestionRandomly(qcmQuestionOnlyList, numberOfQuestionsByLevel);

            //
            return qcmQuestionRandomList;

        }

        public List<Question> selectQCMQuestionOnly(List<Question> questionList )
        {
            // Selectionne dans la liste de question toute les questions qcm
            var qcmQuestionList = questionList
                .Select(question => question)
                .Where(question => (int)question.QuestionType == (int)QuestionTypeEnum.QCM)
                .ToList();


            return qcmQuestionList;
        }

        public List<Question> selectQCMQuestionRandomly
        (
            List<Question> qcmQuestionOnlyList,
            Dictionary<int, double> numberOfQuestionsByLevel
        )
        {
            // Split la liste des question QCM complete en trois listes 
            // En fonction du niveau de question
            var qcmQuestionListJunior = qcmQuestionOnlyList
                .Select(question => question)
                .Where(question => (int)question.QuestionLevel == (int)QuestionLevelEnum.Junior)
                .ToList();

            var qcmQuestionListMedium = qcmQuestionOnlyList
                .Select(question => question)
                .Where(question => (int)question.QuestionLevel == (int)QuestionLevelEnum.Medium)
                .ToList();

            var qcmQuestionListAdvanced = qcmQuestionOnlyList
                .Select(question => question)
                .Where(question => (int)question.QuestionLevel == (int)QuestionLevelEnum.Advanced)
                .ToList();


            var random = new Random();
            List<Question> randomQCMQuestionList = new List<Question>();

            // Pour liste Junior
            for (int i = 0; i < numberOfQuestionsByLevel[(int)QuestionLevelEnum.Junior]; i++)
            {
                int randomNumber = random.Next(qcmQuestionListJunior.Count);
                randomQCMQuestionList.Add(qcmQuestionListJunior[randomNumber]);
                qcmQuestionListJunior.RemoveAt(randomNumber);
            }
            // Pour liste Medium
            for (int i = 0; i < numberOfQuestionsByLevel[(int)QuestionLevelEnum.Medium]; i++)
            {
                int randomNumber = random.Next(qcmQuestionListMedium.Count);
                randomQCMQuestionList.Add(qcmQuestionListMedium[randomNumber]);
                qcmQuestionListMedium.RemoveAt(randomNumber);
            }
            // Pour liste Advanced
            for (int i = 0; i < numberOfQuestionsByLevel[(int)QuestionLevelEnum.Advanced]; i++)
            {
                int randomNumber = random.Next(qcmQuestionListAdvanced.Count);
                randomQCMQuestionList.Add(qcmQuestionListAdvanced[randomNumber]);
                qcmQuestionListAdvanced.RemoveAt(randomNumber);
            }

            return randomQCMQuestionList;
        }



        ////////////////////////////////////////////////////////////////////////////////////////
        ////// 
        ///////////////////////////////////////////////////////////////////////////////////////

        /**
         * Prend en entrée un nombre de question et un niveau de quizz
         * Calcule le pourcentage de questions en fonction du niveau du quizz et du nombre de questions qu'il doit contenir
         * Renvoi un dictionnaire avec { level : nombre de questions }
         */
        public Dictionary<int, double> calculateQuestionNumberByLevel(double totalQuestionNumber, double quizzLevel)
        {
            int[] juniorQuestionLevelPercentage = { 70, 20, 10 };
            int[] mediumQuestionLevelPercentage = { 20, 60, 20 };
            int[] advancedQuestionLevelPercentage = { 10, 40, 50 };

            double juniorQuestionNumber = 0;
            double mediumQuestionNumber = 0;
            double advancedQuestionNumber = 0;

            Dictionary<int, double> numberOfQuestionsByLevel = new Dictionary<int, double>();

            //Junior
            if (quizzLevel == 1)
            {
                // calcule le nombre de questions à intégrer en fonction des pourcentages liées à chaques type
                juniorQuestionNumber = (totalQuestionNumber / 100 ) * juniorQuestionLevelPercentage[0]; 
                mediumQuestionNumber = (totalQuestionNumber / 100) * juniorQuestionLevelPercentage[1];
                advancedQuestionNumber = (totalQuestionNumber / 100) * juniorQuestionLevelPercentage[2];

            }
            // Medium
            else if (quizzLevel == 2)
            {
                juniorQuestionNumber = (totalQuestionNumber / 100) * mediumQuestionLevelPercentage[0];
                mediumQuestionNumber = (totalQuestionNumber / 100) * mediumQuestionLevelPercentage[1];
                advancedQuestionNumber = (totalQuestionNumber / 100) * mediumQuestionLevelPercentage[2];

            }
            //Advanced
            else
            {
                juniorQuestionNumber = (totalQuestionNumber / 100) * advancedQuestionLevelPercentage[0];
                mediumQuestionNumber = (totalQuestionNumber / 100) * advancedQuestionLevelPercentage[1];
                advancedQuestionNumber = (totalQuestionNumber / 100) * advancedQuestionLevelPercentage[2];

            }

            numberOfQuestionsByLevel.Add(1, juniorQuestionNumber);
            numberOfQuestionsByLevel.Add(2, mediumQuestionNumber);
            numberOfQuestionsByLevel.Add(3, advancedQuestionNumber); 

            return numberOfQuestionsByLevel;


        }



    }

}
