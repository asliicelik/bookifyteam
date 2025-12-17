using Microsoft.AspNetCore.Mvc;
using MyMvcProject.Models;
using MyMvcProject.Services;

namespace MyMvcProject.Controllers;

public class BookController : Controller
{
    private readonly IRecommendationService _recommendationService;
    private readonly ILogger<BookController> _logger;

    public BookController(IRecommendationService recommendationService, ILogger<BookController> logger)
    {
        _recommendationService = recommendationService;
        _logger = logger;
    }

    public async Task<IActionResult> Details(int id)
    {
        var book = await _recommendationService.GetBookByIdAsync(id);
        
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    [HttpPost]
    public IActionResult AddToFavorites(int bookId)
    {
        var favorites = GetFavorites();
        
        if (!favorites.Contains(bookId))
        {
            favorites.Add(bookId);
            SaveFavorites(favorites);
            TempData["SuccessMessage"] = "Book added to favorites!";
        }
        else
        {
            TempData["InfoMessage"] = "This book is already in your favorites.";
        }

        return RedirectToAction("Details", new { id = bookId });
    }

    [HttpPost]
    public IActionResult RemoveFromFavorites(int bookId)
    {
        var favorites = GetFavorites();
        favorites.Remove(bookId);
        SaveFavorites(favorites);
        
        TempData["SuccessMessage"] = "Book removed from favorites.";
        
        return RedirectToAction("Favorites");
    }

    public async Task<IActionResult> Favorites()
    {
        var favoriteIds = GetFavorites();
        var favoriteBooks = new List<Book>();

        foreach (var id in favoriteIds)
        {
            var book = await _recommendationService.GetBookByIdAsync(id);
            if (book != null)
            {
                favoriteBooks.Add(book);
            }
        }

        return View(favoriteBooks);
    }

    private List<int> GetFavorites()
    {
        var favoritesJson = HttpContext.Session.GetString("Favorites");
        if (string.IsNullOrEmpty(favoritesJson))
        {
            return new List<int>();
        }

        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<int>>(favoritesJson) ?? new List<int>();
        }
        catch
        {
            return new List<int>();
        }
    }

    private void SaveFavorites(List<int> favorites)
    {
        var favoritesJson = System.Text.Json.JsonSerializer.Serialize(favorites);
        HttpContext.Session.SetString("Favorites", favoritesJson);
    }
}

