using MyMvcProject.Models;

namespace MyMvcProject.Services;

public interface IMoodCatalogProvider
{
    IReadOnlyList<MoodOption> GetAll();
}

public class MoodCatalogProvider : IMoodCatalogProvider
{
    private readonly List<MoodOption> _moods =
    [
        new() { Id = "calm",       DisplayName = "Calm",       Description = "Peaceful, quiet, relaxed" },
        new() { Id = "intense",    DisplayName = "Intense",    Description = "Strong, powerful, energetic" },
        new() { Id = "melancholic",DisplayName = "Melancholic",Description = "Sad, emotional, deep" },
        new() { Id = "focused",    DisplayName = "Focused",    Description = "Concentrated, studying or working" },
        new() { Id = "uplifting",  DisplayName = "Uplifting",  Description = "Joyful, motivating, positive" },
        new() { Id = "nostalgic",  DisplayName = "Nostalgic",  Description = "Full of memories, longing for the past" },
        new() { Id = "romantic",   DisplayName = "Romantic",   Description = "About love, warm and emotional" },
        new() { Id = "rebellious", DisplayName = "Rebellious", Description = "Defiant, bold, against the rules" }
    ];

    public IReadOnlyList<MoodOption> GetAll() => _moods;
}


