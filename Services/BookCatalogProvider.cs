using MyMvcProject.Models;

namespace MyMvcProject.Services;

public interface IBookCatalogProvider
{
    IReadOnlyList<MoodWeightedBook> GetAll();
}

public class BookCatalogProvider : IBookCatalogProvider
{
    // Independent from EF Book entity – this is a mood-weighted catalog for recommendations.
    private readonly List<MoodWeightedBook> _books =
    [
        new()
        {
            Id = "book-lit-1",
            Title = "The Quiet City",
            Author = "Elena Moore",
            Category = "Literary Fiction",
            MoodAffinities =
            {
                ["calm"] = 0.9,
                ["nostalgic"] = 0.6
            }
        },
        new()
        {
            Id = "book-lit-2",
            Title = "Echoes of Yesterday",
            Author = "Elena Moore",
            Category = "Literary Fiction",
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["nostalgic"] = 0.8
            }
        },
        new()
        {
            Id = "book-sf-1",
            Title = "Neon Horizons",
            Author = "Kai Tanaka",
            Category = "Sci-Fi",
            MoodAffinities =
            {
                ["intense"] = 0.85,
                ["rebellious"] = 0.7
            }
        },
        new()
        {
            Id = "book-sf-2",
            Title = "Silent Orbit",
            Author = "Kai Tanaka",
            Category = "Sci-Fi",
            MoodAffinities =
            {
                ["focused"] = 0.7,
                ["calm"] = 0.4
            }
        },
        new()
        {
            Id = "book-rom-1",
            Title = "Under the Old Tree",
            Author = "Mira K.",
            Category = "Romance",
            MoodAffinities =
            {
                ["romantic"] = 0.95,
                ["nostalgic"] = 0.7
            }
        },
        new()
        {
            Id = "book-rom-2",
            Title = "Letters Never Sent",
            Author = "Mira K.",
            Category = "Romance",
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["melancholic"] = 0.75
            }
        },
        new()
        {
            Id = "book-fant-1",
            Title = "Song of the Storm",
            Author = "A. L. Rowan",
            Category = "Fantasy",
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["uplifting"] = 0.6
            }
        },
        new()
        {
            Id = "book-fant-2",
            Title = "The Glass Forest",
            Author = "A. L. Rowan",
            Category = "Fantasy",
            MoodAffinities =
            {
                ["calm"] = 0.6,
                ["focused"] = 0.5
            }
        },
        new()
        {
            Id = "book-nonfic-1",
            Title = "Deep Work Habits",
            Author = "C. Newman",
            Category = "Productivity",
            MoodAffinities =
            {
                ["focused"] = 0.95,
                ["uplifting"] = 0.7
            }
        },
        new()
        {
            Id = "book-nonfic-2",
            Title = "Calm Mind, Busy World",
            Author = "C. Newman",
            Category = "Self-Help",
            MoodAffinities =
            {
                ["calm"] = 0.95,
                ["focused"] = 0.6
            }
        },
        new()
        {
            Id = "book-psy-1",
            Title = "Shades of Blue",
            Author = "Dr. L. Hart",
            Category = "Psychology",
            MoodAffinities =
            {
                ["melancholic"] = 0.95,
                ["nostalgic"] = 0.5
            }
        },
        new()
        {
            Id = "book-psy-2",
            Title = "Joy by Design",
            Author = "Dr. L. Hart",
            Category = "Psychology",
            MoodAffinities =
            {
                ["uplifting"] = 0.95,
                ["calm"] = 0.6
            }
        },
        new()
        {
            Id = "book-music-1",
            Title = "Voices of the Street",
            Author = "R. Demir",
            Category = "Music / Biography",
            MoodAffinities =
            {
                ["rebellious"] = 0.9,
                ["intense"] = 0.8
            }
        },
        new()
        {
            Id = "book-music-2",
            Title = "Songs of Melancholy",
            Author = "R. Demir",
            Category = "Music / Biography",
            MoodAffinities =
            {
                ["melancholic"] = 0.95,
                ["nostalgic"] = 0.8
            }
        },
        new()
        {
            Id = "book-classic-1",
            Title = "The Long Goodbye",
            Author = "R. Chandler",
            Category = "Classic",
            MoodAffinities =
            {
                ["nostalgic"] = 0.85,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "book-classic-2",
            Title = "Brave New Days",
            Author = "A. West",
            Category = "Dystopia",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["rebellious"] = 0.8
            }
        },
        new()
        {
            Id = "book-insp-1",
            Title = "Run Towards the Sun",
            Author = "G. Park",
            Category = "Inspirational",
            MoodAffinities =
            {
                ["uplifting"] = 0.95
            }
        },
        new()
        {
            Id = "book-insp-2",
            Title = "Small Steps",
            Author = "G. Park",
            Category = "Self-Help",
            MoodAffinities =
            {
                ["uplifting"] = 0.9,
                ["calm"] = 0.5
            }
        },
        new()
        {
            Id = "book-essay-1",
            Title = "Against the Noise",
            Author = "S. Arslan",
            Category = "Essays",
            MoodAffinities =
            {
                ["focused"] = 0.8,
                ["rebellious"] = 0.6
            }
        },
        new()
        {
            Id = "book-essay-2",
            Title = "Pieces of Time",
            Author = "S. Arslan",
            Category = "Essays",
            MoodAffinities =
            {
                ["nostalgic"] = 0.9,
                ["calm"] = 0.5
            }
        }
    ];

    public IReadOnlyList<MoodWeightedBook> GetAll() => _books;
}


