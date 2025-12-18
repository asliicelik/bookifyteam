using Microsoft.EntityFrameworkCore;
using MyMvcProject.Data;
using MyMvcProject.Models;

namespace MyMvcProject.Services;

public class RecommendationService : IRecommendationService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RecommendationService> _logger;
    private readonly IMoodCatalogProvider _moodCatalogProvider;
    private readonly ISongCatalogProvider _songCatalogProvider;
    private readonly IBookCatalogProvider _bookCatalogProvider;

    public RecommendationService(
        ApplicationDbContext context,
        ILogger<RecommendationService> logger,
        IMoodCatalogProvider moodCatalogProvider,
        ISongCatalogProvider songCatalogProvider,
        IBookCatalogProvider bookCatalogProvider)
    {
        _context = context;
        _logger = logger;
        _moodCatalogProvider = moodCatalogProvider;
        _songCatalogProvider = songCatalogProvider;
        _bookCatalogProvider = bookCatalogProvider;
    }

    public async Task<string> DetectMoodAsync(string musicInput)
    {
        if (string.IsNullOrWhiteSpace(musicInput))
            return "Calm"; // Default mood

        var inputLower = musicInput.ToLowerInvariant().Trim();
        
        // Get all moods with their keywords
        var moods = await _context.Moods.ToListAsync();
        
        // Score each mood based on keyword matches
        var moodScores = new Dictionary<string, int>();
        
        foreach (var mood in moods)
        {
            int score = 0;
            
            // Check music keywords
            foreach (var keyword in mood.MusicKeywords)
            {
                if (inputLower.Contains(keyword.ToLowerInvariant()))
                {
                    score += 2; // Keywords are more specific
                }
            }
            
            // Check music genres
            foreach (var genre in mood.MusicGenres)
            {
                if (inputLower.Contains(genre.ToLowerInvariant()))
                {
                    score += 1;
                }
            }
            
            // Check mood name itself
            if (inputLower.Contains(mood.Name.ToLowerInvariant()))
            {
                score += 3;
            }
            
            if (score > 0)
            {
                moodScores[mood.Name] = score;
            }
        }
        
        // Return the mood with highest score, or default to "Calm"
        if (moodScores.Any())
        {
            var detectedMood = moodScores.OrderByDescending(x => x.Value).First().Key;
            _logger.LogInformation("Detected mood: {Mood} for input: {Input}", detectedMood, musicInput);
            return detectedMood;
        }
        
        // Fallback: try to detect based on common patterns
        if (inputLower.Contains("rock") || inputLower.Contains("metal") || inputLower.Contains("punk"))
            return "Intense";
        
        if (inputLower.Contains("sad") || inputLower.Contains("melancholic") || inputLower.Contains("deep"))
            return "Melancholic";
        
        if (inputLower.Contains("focus") || inputLower.Contains("study") || inputLower.Contains("work") || inputLower.Contains("concentration"))
            return "Focused";
        
        if (inputLower.Contains("happy") || inputLower.Contains("joy") || inputLower.Contains("upbeat") || inputLower.Contains("uplifting"))
            return "Uplifting";
        
        // Default
        return "Calm";
    }

    public async Task<List<Book>> GetRecommendationsAsync(string mood, int count = 10)
    {
        var books = await _context.Books
            .Where(b => b.Mood == mood)
            .OrderBy(b => Guid.NewGuid()) // Randomize
            .Take(count)
            .ToListAsync();
        
        return books;
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public List<SongResult> RecommendSongs(List<string> moodIds, int top = 5)
    {
        var normalizedMoods = NormalizeMoodIds(moodIds);
        var weights = BuildWeights(normalizedMoods);

        if (!weights.Any())
        {
            return new List<SongResult>();
        }

        var songs = _songCatalogProvider.GetAll();

        var scored = songs
            .Select(song => new SongResult
            {
                Song = song,
                Score = ComputeScore(song.MoodAffinities, weights)
            })
            .Where(r => r.Score > 0)
            .OrderByDescending(r => r.Score)
            .ThenBy(r => r.Song.Title)
            .Take(top)
            .ToList();

        if (scored.Count == 0)
        {
            // Fallback: take top songs by any affinity with closest moods (simply max affinity).
            scored = songs
                .Select(song => new SongResult
                {
                    Song = song,
                    Score = song.MoodAffinities
                        .Where(kv => weights.ContainsKey(kv.Key))
                        .Select(kv => kv.Value)
                        .DefaultIfEmpty(0)
                        .Max()
                })
                .Where(r => r.Score > 0)
                .OrderByDescending(r => r.Score)
                .ThenBy(r => r.Song.Title)
                .Take(top)
                .ToList();
        }

        return scored;
    }

    public BookResult? RecommendBook(List<string> moodIds)
    {
        var normalizedMoods = NormalizeMoodIds(moodIds);
        var weights = BuildWeights(normalizedMoods);

        if (!weights.Any())
        {
            return null;
        }

        var books = _bookCatalogProvider.GetAll();

        var scored = books
            .Select(book => new BookResult
            {
                Book = book,
                Score = ComputeScore(book.MoodAffinities, weights)
            })
            .Where(r => r.Score > 0)
            .OrderByDescending(r => r.Score)
            .ThenBy(r => r.Book!.Title)
            .FirstOrDefault();

        if (scored == null || scored.Score <= 0)
        {
            // Fallback to the closest by max affinity.
            scored = books
                .Select(book => new BookResult
                {
                    Book = book,
                    Score = book.MoodAffinities
                        .Where(kv => weights.ContainsKey(kv.Key))
                        .Select(kv => kv.Value)
                        .DefaultIfEmpty(0)
                        .Max()
                })
                .Where(r => r.Score > 0)
                .OrderByDescending(r => r.Score)
                .ThenBy(r => r.Book!.Title)
                .FirstOrDefault();
        }

        return scored;
    }

    private static List<string> NormalizeMoodIds(List<string> moodIds)
    {
        return moodIds
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Select(id => id.Trim().ToLowerInvariant())
            .Distinct()
            .ToList();
    }

    private static Dictionary<string, double> BuildWeights(List<string> moodIds)
    {
        if (moodIds.Count == 0)
        {
            return new Dictionary<string, double>();
        }

        var count = Math.Min(3, moodIds.Count);
        double weight = count switch
        {
            1 => 1.0,
            2 => 0.5,
            3 => 1.0 / 3.0,
            _ => 1.0 / count
        };

        return moodIds
            .Take(3)
            .ToDictionary(id => id, _ => weight);
    }

    private static double ComputeScore(
        IReadOnlyDictionary<string, double> affinities,
        IReadOnlyDictionary<string, double> weights)
    {
        double score = 0;
        foreach (var (moodId, weight) in weights)
        {
            if (affinities.TryGetValue(moodId, out var affinity))
            {
                score += weight * affinity;
            }
        }

        return score;
    }
}

