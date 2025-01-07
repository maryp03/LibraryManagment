using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagment.Data;
using LibraryManagment.Models;
using Microsoft.Data.SqlClient;

namespace LibraryManagment.Controllers
{
    public class BorrowingsController : Controller
    {
        private readonly LibraryManagmentContext _context;

        public BorrowingsController(LibraryManagmentContext context)
        {
            _context = context;
        }

        // GET: Borrowings
        public async Task<IActionResult> Index()
        {
            var libraryManagmentContext = _context.Borrowing.Include(b => b.Book).Include(b => b.User);
            return View(await libraryManagmentContext.ToListAsync());
        }

        // GET: Borrowings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // GET: Borrowings/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FullName");
            return View();
        }

        // POST: Borrowings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,BookId,DateBorrowed,DateReturned")] Borrowing borrowing)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation error: {modelError.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.BorrowBookAsync(borrowing.UserId, borrowing.BookId);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", borrowing.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FullName", borrowing.UserId);
            return View(borrowing);
        }

        // GET: Borrowings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", borrowing.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FullName", borrowing.UserId);
            return View(borrowing);
        }

        // POST: Borrowings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,BookId,DateBorrowed,DateReturned")] Borrowing borrowing)
        {
            if (id != borrowing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldBookId = _context.Borrowing
                        .Where(b => b.Id == id)
                        .Select(b => b.BookId)
                        .FirstOrDefault();

                    if (oldBookId != borrowing.BookId)
                    {
                        await _context.UpdateBorrowingAsync(borrowing.Id, borrowing.UserId, oldBookId, borrowing.BookId);
                    }
                    else
                    {
                        _context.Update(borrowing);
                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (SqlException ex) when (ex.Message.Contains("The new book is not available"))
                {
                    ModelState.AddModelError("", "The selected book is no longer available.");
                    ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", borrowing.BookId);
                    ViewData["UserId"] = new SelectList(_context.User, "Id", "FullName", borrowing.UserId);
                    return View(borrowing);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowingExists(borrowing.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", borrowing.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FullName", borrowing.UserId);
            return View(borrowing);
        }

        // GET: Borrowings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // POST: Borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!BorrowingExists(id))
            {
                return NotFound();
            }

            try
            {
                await _context.ReturnBookAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Borrowing record not found.") == true)
                {
                    ModelState.AddModelError("", "Borrowing record not found.");
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while returning the book.");
                }
                return RedirectToAction(nameof(Index));
            }
        }
        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.Id == id);
        }
    }
}