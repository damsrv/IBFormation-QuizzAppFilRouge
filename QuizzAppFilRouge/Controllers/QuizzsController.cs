using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // Constructor
        public QuizzsController(IQuizzRepository quizzsRepository)
        {
            this.quizzsRepository = quizzsRepository;
        }

//////////////////////////////////////////////////////////////////////////////////////
//// INDEX AND GETQUIZZS FUNCTIONS 
//////////////////////////////////////////////////////////////////////////////////////

        //OK
        public IActionResult Index()
        {
            
            return View();

        }

        //OK
        // GET: Quizzs
        public IActionResult GetAllQuizzs()
        {

            var quizzes = quizzsRepository.GetAll();
              
            return View(quizzes);

        }


//////////////////////////////////////////////////////////////////////////////////////
//// DETAILS FUNCTIONS 
//////////////////////////////////////////////////////////////////////////////////////

        // OK
        // GET: Quizzs/Details/5
        public IActionResult Details(int? id)
        {
            var quizz = quizzsRepository.Details(id);

            if (quizz == null)
            {
                return NotFound();
            }

            return View(quizz);
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        ////// CREATE FUNCTIONS GET AND POST
        ////////////////////////////////////////////////////////////////////////////////////////

        //        // GET: Quizzs/Create
        //        public IActionResult Create()
        //        {
        //            return View();
        //        }


        //        // POST: Quizzs/Create
        //        // To protect from overposting attacks, enable the specific properties you want to bind to.
        //        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Create(QuizzViewModel quizzViewModel)
        //        {
        //            // création du guid
        //            var guid = Guid.NewGuid().ToString();

        //            // TODO : Function qui Créer des questions 
        //            // 


        //            //if (ModelState.IsValid)
        //            //{
        //            //    _context.Add(quizz);
        //            //    await _context.SaveChangesAsync();
        //            //    return RedirectToAction(nameof(Index));
        //            //}
        //            //return View(quizz);
        //            return View();
        //        }



        ////////////////////////////////////////////////////////////////////////////////////////
        ////// EDIT FUNCTIONS GET AND POST
        ////////////////////////////////////////////////////////////////////////////////////////

        //OK
        // GET: Quizzs/Edit/5
        public IActionResult Edit(int? id)
        {
            var quizz = quizzsRepository.GetQuizzById(id);

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
        public IActionResult Edit(QuizzViewModel quizzViewModel)
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
                    if (!quizzsRepository.QuizzExists(quizz.Id))
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
        ////// DELETE FUNCTIONS GET AND POST
        ////////////////////////////////////////////////////////////////////////////////////////

        //        // GET: Quizzs/Delete/5
        //        public async Task<IActionResult> Delete(int? id)
        //        {
        //            if (id == null || _context.Quizzes == null)
        //            {
        //                return NotFound();
        //            }

        //            var quizz = await _context.Quizzes
        //                .FirstOrDefaultAsync(m => m.Id == id);
        //            if (quizz == null)
        //            {
        //                return NotFound();
        //            }

        //            return View(quizz);
        //        }

        //        // POST: Quizzs/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> DeleteConfirmed(int id)
        //        {
        //            if (_context.Quizzes == null)
        //            {
        //                return Problem("Entity set 'ApplicationDbContext.Quizzes'  is null.");
        //            }
        //            var quizz = await _context.Quizzes.FindAsync(id);
        //            if (quizz != null)
        //            {
        //                _context.Quizzes.Remove(quizz);
        //            }

        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }








    }





}
