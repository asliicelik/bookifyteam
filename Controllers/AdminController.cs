using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcProject.Data;
using MyMvcProject.Models;

namespace MyMvcProject.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AdminController> _logger;

    public AdminController(ApplicationDbContext context, ILogger<AdminController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Admin
    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.OrderBy(b => b.Title).ToListAsync();
        return View(books);
    }

    // GET: Admin/Create
    public IActionResult Create()
    {
        ViewBag.Moods = new List<string> { "Calm", "Intense", "Melancholic", "Focused", "Uplifting" };
        return View();
    }

    // POST: Admin/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        if (ModelState.IsValid)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Book added successfully!";
            return RedirectToAction(nameof(Index));
        }
        
        ViewBag.Moods = new List<string> { "Calm", "Intense", "Melancholic", "Focused", "Uplifting" };
        return View(book);
    }

    // GET: Admin/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        
        ViewBag.Moods = new List<string> { "Calm", "Intense", "Melancholic", "Focused", "Uplifting" };
        return View(book);
    }

    // POST: Admin/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book book)
    {
        if (id != book.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Book updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.Id))
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
        
        ViewBag.Moods = new List<string> { "Calm", "Intense", "Melancholic", "Focused", "Uplifting" };
        return View(book);
    }

    // GET: Admin/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .FirstOrDefaultAsync(m => m.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    // POST: Admin/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Book deleted successfully!";
        }

        return RedirectToAction(nameof(Index));
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}

