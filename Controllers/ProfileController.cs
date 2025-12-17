using Microsoft.AspNetCore.Mvc;
using MyMvcProject.Models;

namespace MyMvcProject.Controllers;

public class ProfileController : Controller
{
    public IActionResult Index()
    {
        var preferences = new UserPreferences
        {
            RecentInputs = GetRecentInputs(),
            FavoriteGenres = GetFavoriteGenres(),
            FavoriteMoods = GetFavoriteMoods()
        };

        return View(preferences);
    }

    [HttpPost]
    public IActionResult UpdatePreferences(UserPreferences preferences)
    {
        SaveRecentInputs(preferences.RecentInputs);
        SaveFavoriteGenres(preferences.FavoriteGenres);
        SaveFavoriteMoods(preferences.FavoriteMoods);

        TempData["SuccessMessage"] = "Your preferences have been updated!";
        return RedirectToAction("Index");
    }

    private List<string> GetRecentInputs()
    {
        var json = HttpContext.Session.GetString("RecentInputs");
        if (string.IsNullOrEmpty(json))
            return new List<string>();

        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }

    private void SaveRecentInputs(List<string> inputs)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(inputs);
        HttpContext.Session.SetString("RecentInputs", json);
    }

    private List<string> GetFavoriteGenres()
    {
        var json = HttpContext.Session.GetString("FavoriteGenres");
        if (string.IsNullOrEmpty(json))
            return new List<string>();

        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }

    private void SaveFavoriteGenres(List<string> genres)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(genres);
        HttpContext.Session.SetString("FavoriteGenres", json);
    }

    private List<string> GetFavoriteMoods()
    {
        var json = HttpContext.Session.GetString("FavoriteMoods");
        if (string.IsNullOrEmpty(json))
            return new List<string>();

        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }

    private void SaveFavoriteMoods(List<string> moods)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(moods);
        HttpContext.Session.SetString("FavoriteMoods", json);
    }
}

