using Microsoft.AspNetCore.Mvc;
using MyMvcProject.Models;
using MyMvcProject.Services;

namespace MyMvcProject.Controllers;

public class ListeningController : Controller
{
    private readonly ISongCatalogProvider _songCatalogProvider;
    private readonly IRecommendationService _recommendationService;

    public ListeningController(
        ISongCatalogProvider songCatalogProvider,
        IRecommendationService recommendationService)
    {
        _songCatalogProvider = songCatalogProvider;
        _recommendationService = recommendationService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var songs = _songCatalogProvider.GetAll();
        var artistNames = songs
            .Select(s => s.Artist)
            .Where(a => !string.IsNullOrWhiteSpace(a))
            .Distinct(StringComparer.InvariantCultureIgnoreCase)
            .OrderBy(a => a)
            .ToList();

        var vm = new ListeningHistoryViewModel
        {
            Artists = artistNames
                .Select(name => new ArtistOptionViewModel
                {
                    Id = name,
                    Name = name,
                    IsSelected = false
                })
                .ToList(),
            Songs = songs
                .Select(s => new SongOptionViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Artist = s.Artist,
                    IsSelected = false
                })
                .OrderBy(s => s.Artist)
                .ThenBy(s => s.Title)
                .ToList()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Recommend(ListeningHistoryViewModel model)
    {
        model.SelectedArtistIds = model.SelectedArtistIds
            .Where(a => !string.IsNullOrWhiteSpace(a))
            .Select(a => a.Trim())
            .Distinct(StringComparer.InvariantCultureIgnoreCase)
            .ToList();

        model.SelectedSongIds = model.SelectedSongIds
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Select(id => id.Trim())
            .Distinct(StringComparer.InvariantCultureIgnoreCase)
            .ToList();

        if (!model.SelectedArtistIds.Any() && !model.SelectedSongIds.Any())
        {
            ModelState.AddModelError(nameof(model.SelectedArtistIds), "Select at least one artist or song.");
        }

        if (model.SelectedArtistIds.Count > 20)
        {
            ModelState.AddModelError(nameof(model.SelectedArtistIds), "You can select at most 20 artists.");
        }

        var songs = _songCatalogProvider.GetAll();
        var artistNames = songs
            .Select(s => s.Artist)
            .Where(a => !string.IsNullOrWhiteSpace(a))
            .Distinct(StringComparer.InvariantCultureIgnoreCase)
            .OrderBy(a => a)
            .ToList();

        if (!ModelState.IsValid)
        {
            var invalidVm = BuildViewModel(artistNames, songs, model.SelectedArtistIds, model.SelectedSongIds, null, "Please fix validation errors and try again.");
            return View("Index", invalidVm);
        }

        var bookResult = _recommendationService.RecommendBookFromListeningHistory(
            model.SelectedArtistIds,
            model.SelectedSongIds);

        string? message = null;
        if (bookResult == null || bookResult.Score <= 0)
        {
            message = "No matching book found for this listening history. Try a different combination.";
        }

        var vm = BuildViewModel(artistNames, songs, model.SelectedArtistIds, model.SelectedSongIds, bookResult, message);

        return View("Index", vm);
    }

    private static ListeningHistoryViewModel BuildViewModel(
        List<string> artistNames,
        IReadOnlyList<Song> songs,
        List<string> selectedArtistIds,
        List<string> selectedSongIds,
        BookResult? bookResult,
        string? message)
    {
        var artistSet = new HashSet<string>(selectedArtistIds, StringComparer.InvariantCultureIgnoreCase);
        var songSet = new HashSet<string>(selectedSongIds, StringComparer.InvariantCultureIgnoreCase);

        var vm = new ListeningHistoryViewModel
        {
            SelectedArtistIds = selectedArtistIds,
            SelectedSongIds = selectedSongIds,
            Artists = artistNames
                .Select(name => new ArtistOptionViewModel
                {
                    Id = name,
                    Name = name,
                    IsSelected = artistSet.Contains(name)
                })
                .ToList(),
            Songs = songs
                .Select(s => new SongOptionViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Artist = s.Artist,
                    IsSelected = songSet.Contains(s.Id)
                })
                .OrderBy(s => s.Artist)
                .ThenBy(s => s.Title)
                .ToList(),
            BookResult = bookResult,
            ValidationMessage = message
        };

        return vm;
    }
}


