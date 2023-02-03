using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;

namespace QuizzAppFilRouge.Controllers
{
    public class ResponsesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResponsesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Responses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Responses.Include(r => r.IdentityUser).Include(r => r.Question).Include(r => r.Quizz);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Responses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Responses == null)
            {
                return NotFound();
            }

            var response = await _context.Responses
                .Include(r => r.IdentityUser)
                .Include(r => r.Question)
                .Include(r => r.Quizz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        // GET: Responses/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id");
            ViewData["QuizzId"] = new SelectList(_context.Quizzes, "Id", "Id");
            return View();
        }

        // POST: Responses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,QuestionId,IdentityUserId,QuizzId")] Response response)
        {
            if (ModelState.IsValid)
            {
                _context.Add(response);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", response.IdentityUserId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", response.QuestionId);
            ViewData["QuizzId"] = new SelectList(_context.Quizzes, "Id", "Id", response.QuizzId);
            return View(response);
        }

        // GET: Responses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Responses == null)
            {
                return NotFound();
            }

            var response = await _context.Responses.FindAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", response.IdentityUserId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", response.QuestionId);
            ViewData["QuizzId"] = new SelectList(_context.Quizzes, "Id", "Id", response.QuizzId);
            return View(response);
        }

        // POST: Responses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,QuestionId,IdentityUserId,QuizzId")] Response response)
        {
            if (id != response.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(response);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponseExists(response.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", response.IdentityUserId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", response.QuestionId);
            ViewData["QuizzId"] = new SelectList(_context.Quizzes, "Id", "Id", response.QuizzId);
            return View(response);
        }

        // GET: Responses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Responses == null)
            {
                return NotFound();
            }

            var response = await _context.Responses
                .Include(r => r.IdentityUser)
                .Include(r => r.Question)
                .Include(r => r.Quizz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        // POST: Responses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Responses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Responses'  is null.");
            }
            var response = await _context.Responses.FindAsync(id);
            if (response != null)
            {
                _context.Responses.Remove(response);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponseExists(int id)
        {
          return (_context.Responses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
