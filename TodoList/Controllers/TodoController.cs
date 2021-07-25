using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoListContext _context;

        public TodoController(TodoListContext context)
        {
            _context = context;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
            return View(await _context.TodoModel.ToListAsync());
        }

        // GET: Todo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoModel = await _context.TodoModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoModel == null)
            {
                return NotFound();
            }

            return View(todoModel);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CreateDate,ModifyDate")] TodoModel todoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoModel);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoModel = await _context.TodoModel.FindAsync(id);
            if (todoModel == null)
            {
                return NotFound();
            }
            return View(todoModel);
        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CreateDate,ModifyDate")] TodoModel todoModel)
        {
            if (id != todoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoModelExists(todoModel.Id))
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
            return View(todoModel);
        }

        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoModel = await _context.TodoModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoModel == null)
            {
                return NotFound();
            }

            return View(todoModel);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoModel = await _context.TodoModel.FindAsync(id);
            _context.TodoModel.Remove(todoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoModelExists(int id)
        {
            return _context.TodoModel.Any(e => e.Id == id);
        }
    }
}
