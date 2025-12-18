using System.ComponentModel.DataAnnotations;

namespace MyMvcProject.Models;

/// <summary>
/// Lightweight mood model for the in-memory recommendation catalog.
/// This does NOT affect the EF-backed Mood entity used elsewhere.
/// </summary>
public class MoodOption
{
    [Required]
    public string Id { get; set; } = string.Empty; // e.g. "calm", "intense"

    [Required]
    public string DisplayName { get; set; } = string.Empty; // e.g. "Calm"

    public string? Description { get; set; }
}

public class Song
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public string? Genre { get; set; }
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Mood affinities in the range [0,1], keyed by MoodOption.Id.
    /// </summary>
    public Dictionary<string, double> MoodAffinities { get; set; } = new();
}

public class MoodWeightedBook
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Mood affinities in the range [0,1], keyed by MoodOption.Id.
    /// </summary>
    public Dictionary<string, double> MoodAffinities { get; set; } = new();
}

public class SongResult
{
    public Song Song { get; set; } = new();
    public double Score { get; set; }
}

public class BookResult
{
    public MoodWeightedBook? Book { get; set; }
    public double Score { get; set; }
}

public class RecommendationViewModel
{
    /// <summary>
    /// All mood options with selection info for the UI.
    /// </summary>
    public List<MoodOptionViewModel> Moods { get; set; } = new();

    /// <summary>
    /// Raw selected mood ids from the form post.
    /// </summary>
    [Display(Name = "Moods")]
    [Required(ErrorMessage = "Please select at least one mood.")]
    public List<string> SelectedMoodIds { get; set; } = new();

    public List<SongResult> SongResults { get; set; } = new();
    public BookResult? BookResult { get; set; }

    public string? ValidationMessage { get; set; }
}

public class MoodOptionViewModel
{
    public string Id { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public bool IsSelected { get; set; }
}


