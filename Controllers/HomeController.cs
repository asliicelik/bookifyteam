using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcProject.Models;
using MyMvcProject.Services;

namespace MyMvcProject.Controllers;

public class HomeController : Controller
{
    private readonly IRecommendationService _recommendationService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IRecommendationService recommendationService, ILogger<HomeController> logger)
    {
        _recommendationService = recommendationService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Redirect home page directly to the mood selector
        return RedirectToAction("Index", "Recommendation");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GetRecommendations(MusicInputViewModel model)
    {
        _logger.LogInformation("GetRecommendations POST called with input: {Input}", model?.Input);
        
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("ModelState is invalid. Errors: {Errors}", 
                string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
            return View("Index", model);
        }

        if (string.IsNullOrWhiteSpace(model?.Input))
        {
            _logger.LogWarning("Input is null or empty");
            ModelState.AddModelError("Input", "Please enter a music input");
            return View("Index", model);
        }

        return await ProcessRecommendations(model.Input);
    }

    [HttpGet]
    public async Task<IActionResult> GetRecommendations(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return RedirectToAction("Index");
        }

        return await ProcessRecommendations(input);
    }

    private async Task<IActionResult> ProcessRecommendations(string musicInput)
    {
        try
        {
            _logger.LogInformation("Processing recommendations for input: {Input}", musicInput);
            
            // Detect mood from music input
            var detectedMood = await _recommendationService.DetectMoodAsync(musicInput);
            _logger.LogInformation("Detected mood: {Mood}", detectedMood);
            
            // Get book recommendations
            var recommendedBooks = await _recommendationService.GetRecommendationsAsync(detectedMood, 10);
            _logger.LogInformation("Found {Count} books for mood: {Mood}", recommendedBooks.Count, detectedMood);
            
            // Save to user preferences (recent inputs)
            SaveRecentInput(musicInput);
            
            var viewModel = new BookRecommendationViewModel
            {
                DetectedMood = detectedMood ?? "Calm",
                InputText = musicInput ?? string.Empty,
                RecommendedBooks = recommendedBooks ?? new List<Book>(),
                TotalCount = recommendedBooks?.Count ?? 0
            };

            _logger.LogInformation("Returning Recommendations view with {Count} books", viewModel.RecommendedBooks.Count);
            return View("Recommendations", viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting recommendations for input: {Input}. Exception: {Exception}", musicInput, ex.Message);
            ModelState.AddModelError("", $"An error occurred while getting recommendations: {ex.Message}. Please try again.");
            return View("Index", new MusicInputViewModel { Input = musicInput });
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private void SaveRecentInput(string input)
    {
        var recentInputs = GetRecentInputs();
        recentInputs.Insert(0, input);
        
        // Keep only last 10 inputs
        if (recentInputs.Count > 10)
        {
            recentInputs = recentInputs.Take(10).ToList();
        }
        
        var json = System.Text.Json.JsonSerializer.Serialize(recentInputs);
        HttpContext.Session.SetString("RecentInputs", json);
    }

    private List<string> GetRecentInputs()
    {
        var json = HttpContext.Session.GetString("RecentInputs");
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
}
