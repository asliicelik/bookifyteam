using Microsoft.EntityFrameworkCore;
using MyMvcProject.Data;
using MyMvcProject.Models;

namespace MyMvcProject.Services;

public class RecommendationService : IRecommendationService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RecommendationService> _logger;

    public RecommendationService(ApplicationDbContext context, ILogger<RecommendationService> logger)
    {
        _context = context;
        _logger = logger;
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
}

