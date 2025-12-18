using Microsoft.AspNetCore.Mvc;
using MyMvcProject.Models;
using MyMvcProject.Services;

namespace MyMvcProject.Controllers;

public class RecommendationController : Controller
{
    private const string SessionKeySelectedMoods = "SelectedMoodIds";

    private readonly IMoodCatalogProvider _moodCatalogProvider;
    private readonly IRecommendationService _recommendationService;

    public RecommendationController(
        IMoodCatalogProvider moodCatalogProvider,
        IRecommendationService recommendationService)
    {
        _moodCatalogProvider = moodCatalogProvider;
        _recommendationService = recommendationService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var selected = GetSelectedMoodsFromSession();
        var vm = BuildViewModel(selected, null, null, null);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Recommend(RecommendationViewModel model)
    {
        model.SelectedMoodIds = model.SelectedMoodIds
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Select(id => id.Trim().ToLowerInvariant())
            .Distinct()
            .ToList();

        if (!model.SelectedMoodIds.Any())
        {
            ModelState.AddModelError(nameof(model.SelectedMoodIds), "Please select at least one mood.");
        }

        if (model.SelectedMoodIds.Count > 3)
        {
            ModelState.AddModelError(nameof(model.SelectedMoodIds), "You can select at most 3 moods.");
        }

        if (!ModelState.IsValid)
        {
            var invalidVm = BuildViewModel(
                model.SelectedMoodIds,
                null,
                null,
                "Please fix the validation errors and try again.");
            return View("Index", invalidVm);
        }

        SaveSelectedMoodsToSession(model.SelectedMoodIds);

        var songResults = _recommendationService.RecommendSongs(model.SelectedMoodIds, 5);
        var bookResult = _recommendationService.RecommendBook(model.SelectedMoodIds);

        string? message = null;
        if (!songResults.Any() && (bookResult == null || bookResult.Score <= 0))
        {
            message = "No recommendations were found for this mood combination. Please try different moods.";
        }

        var vm = BuildViewModel(model.SelectedMoodIds, songResults, bookResult, message);

        return View("Index", vm);
    }

    private RecommendationViewModel BuildViewModel(
        List<string> selectedMoodIds,
        List<SongResult>? songResults,
        BookResult? bookResult,
        string? message)
    {
        var allMoods = _moodCatalogProvider.GetAll();

        var moodOptions = allMoods
            .Select(m => new MoodOptionViewModel
            {
                Id = m.Id,
                DisplayName = m.DisplayName,
                Description = m.Description,
                IsSelected = selectedMoodIds.Contains(m.Id)
            })
            .ToList();

        return new RecommendationViewModel
        {
            Moods = moodOptions,
            SelectedMoodIds = selectedMoodIds,
            SongResults = songResults ?? new List<SongResult>(),
            BookResult = bookResult,
            ValidationMessage = message
        };
    }

    private List<string> GetSelectedMoodsFromSession()
    {
        var json = HttpContext.Session.GetString(SessionKeySelectedMoods);
        if (string.IsNullOrEmpty(json))
        {
            return new List<string>();
        }

        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }

    private void SaveSelectedMoodsToSession(List<string> moods)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(moods);
        HttpContext.Session.SetString(SessionKeySelectedMoods, json);
    }
}


