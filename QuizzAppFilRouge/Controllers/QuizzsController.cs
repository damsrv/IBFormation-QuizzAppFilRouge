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
using QuizzAppFilRouge.Models.QuizzViewModel;

namespace QuizzAppFilRouge.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuizzsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor
        public QuizzsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            return View();

        }

        // GET: Quizzs
        public async Task<IActionResult> GetQuizzs()
        {
            var quizzes = await _context.Quizzes.ToListAsync();
            return View(quizzes);

        }


//////////////////////////////////////////////////////////////////////////////////////
//// DETAILS FUNCTIONS 
//////////////////////////////////////////////////////////////////////////////////////

        // GET: Quizzs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Quizzes == null)
            {
                return NotFound();
            }

            var quizz = await _context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizz == null)
            {
                return NotFound();
            }

            return View(quizz);
        }

//////////////////////////////////////////////////////////////////////////////////////
//// CREATE FUNCTIONS GET AND POST
//////////////////////////////////////////////////////////////////////////////////////

        // GET: Quizzs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quizzs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Notation,ValidationCode")] Quizz quizz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quizz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quizz);
        }



//////////////////////////////////////////////////////////////////////////////////////
//// EDIT FUNCTIONS GET AND POST
//////////////////////////////////////////////////////////////////////////////////////

        // GET: Quizzs/Edit/5
        public IActionResult Edit(int? id)
        {

            if (id == null || _context.Quizzes == null)
            {
                return NotFound();
            }


            var quizz = _context.Quizzes
                .Select(m => m)
                .Where(m => m.Id == id)
                .Include(p => );


            //var quizz =
            //     from quizzs in _context.Quizzes
            //     join questionQuizzs in _context.QuestionQuizzs on quizzs.Id equals questionQuizzs.
            //     select new
            //     {
            //         quizzInfo = quizzs,
            //         questionsQuizz = questionQuizzs
            //     };



            if (quizz == null)
            {
                return NotFound();
            }
            //var quizzViewModel = new QuizzViewModel
            //{
            //    Id = quizz.Id,
            //    Notation = quizz.Notation,
            //    ValidationCode = quizz.ValidationCode,
            //};

            return View(quizz);

        }

        // OK
        // POST: Quizzs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuizzViewModel quizzViewModel)
        {

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
                    _context.Update(quizz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizzExists(quizz.Id))
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
            var errors = ModelState.Select(x => x.Value.Errors)
                       .Where(y => y.Count > 0)
                       .ToList();
            return RedirectToAction(nameof(Index));
        }


//////////////////////////////////////////////////////////////////////////////////////
//// DELETE FUNCTIONS GET AND POST
//////////////////////////////////////////////////////////////////////////////////////

        // GET: Quizzs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Quizzes == null)
            {
                return NotFound();
            }

            var quizz = await _context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizz == null)
            {
                return NotFound();
            }

            return View(quizz);
        }

        // POST: Quizzs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Quizzes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Quizzes'  is null.");
            }
            var quizz = await _context.Quizzes.FindAsync(id);
            if (quizz != null)
            {
                _context.Quizzes.Remove(quizz);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizzExists(int id)
        {
          return (_context.Quizzes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
