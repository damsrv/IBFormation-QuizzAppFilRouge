﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Domain;

namespace QuizzAppFilRouge.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IQuestionRepository questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;   
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var questions = await questionRepository.GetAllQuestions();

            return questionRepository.GetAllQuestions() != null ? 
                          View() :
                          Problem("Entity set 'ApplicationDbContext.Questions'  is null.");
        }

        //// GET: Questions/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Questions == null)
        //    {
        //        return NotFound();
        //    }

        //    var question = await _context.Questions
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(question);
        //}

        //// GET: Questions/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Questions/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Content,QuestionLevel,QuestionType")] Question question)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(question);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(question);
        //}

        //// GET: Questions/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Questions == null)
        //    {
        //        return NotFound();
        //    }

        //    var question = await _context.Questions.FindAsync(id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(question);
        //}

        //// POST: Questions/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Content,QuestionLevel,QuestionType")] Question question)
        //{
        //    if (id != question.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(question);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!QuestionExists(question.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(question);
        //}

        //// GET: Questions/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Questions == null)
        //    {
        //        return NotFound();
        //    }

        //    var question = await _context.Questions
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(question);
        //}

        //// POST: Questions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Questions == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Questions'  is null.");
        //    }
        //    var question = await _context.Questions.FindAsync(id);
        //    if (question != null)
        //    {
        //        _context.Questions.Remove(question);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool QuestionExists(int id)
        //{
        //  return (_context.Questions?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
