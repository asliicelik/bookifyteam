using MyMvcProject.Models;

namespace MyMvcProject.Services;

public interface ISongCatalogProvider
{
    IReadOnlyList<Song> GetAll();
}

public class SongCatalogProvider : ISongCatalogProvider
{
    // In-memory seeded song catalog.
    // At least 30 songs, including multiple songs by the same artist (e.g., Müslüm Gürses).
    private readonly List<Song> _songs =
    [
        // Calm / Focused / Uplifting
        new()
        {
            Id = "song-lofi-1",
            Title = "Midnight Lofi",
            Artist = "Loftwave",
            Genre = "Lo-fi",
            Tags = ["study", "chill"],
            MoodAffinities = new Dictionary<string, double>
            {
                ["calm"] = 0.9,
                ["focused"] = 0.8,
                ["uplifting"] = 0.3
            }
        },
        new()
        {
            Id = "song-lofi-2",
            Title = "Rainy Window",
            Artist = "Loftwave",
            Genre = "Lo-fi",
            Tags = ["rain", "cozy"],
            MoodAffinities =
            {
                ["calm"] = 0.95,
                ["melancholic"] = 0.4,
                ["nostalgic"] = 0.6
            }
        },
        new()
        {
            Id = "song-focus-1",
            Title = "Deep Focus",
            Artist = "NeuroBeats",
            Genre = "Ambient",
            Tags = ["focus", "work"],
            MoodAffinities =
            {
                ["focused"] = 0.95,
                ["calm"] = 0.6
            }
        },
        new()
        {
            Id = "song-focus-2",
            Title = "Coding Session",
            Artist = "NeuroBeats",
            Genre = "Electronic",
            Tags = ["coding", "flow"],
            MoodAffinities =
            {
                ["focused"] = 0.9,
                ["uplifting"] = 0.4
            }
        },
        new()
        {
            Id = "song-happy-1",
            Title = "Sunny Morning",
            Artist = "Aurora Days",
            Genre = "Indie Pop",
            Tags = ["happy"],
            MoodAffinities =
            {
                ["uplifting"] = 0.95,
                ["calm"] = 0.4
            }
        },

        // Intense / Rebellious
        new()
        {
            Id = "song-metal-1",
            Title = "Burning Night",
            Artist = "Iron Pulse",
            Genre = "Metal",
            Tags = ["heavy", "intense"],
            MoodAffinities =
            {
                ["intense"] = 0.95,
                ["rebellious"] = 0.7
            }
        },
        new()
        {
            Id = "song-rock-1",
            Title = "Runaway City",
            Artist = "Iron Pulse",
            Genre = "Rock",
            Tags = ["fast"],
            MoodAffinities =
            {
                ["intense"] = 0.85,
                ["uplifting"] = 0.5,
                ["rebellious"] = 0.5
            }
        },
        new()
        {
            Id = "song-punk-1",
            Title = "No Rules Tonight",
            Artist = "Street Lights",
            Genre = "Punk",
            Tags = ["rebel"],
            MoodAffinities =
            {
                ["rebellious"] = 0.95,
                ["intense"] = 0.8
            }
        },

        // Melancholic / Nostalgic
        new()
        {
            Id = "song-piano-1",
            Title = "Falling Leaves",
            Artist = "Quiet Keys",
            Genre = "Classical",
            Tags = ["sad", "autumn"],
            MoodAffinities =
            {
                ["melancholic"] = 0.95,
                ["nostalgic"] = 0.8
            }
        },
        new()
        {
            Id = "song-piano-2",
            Title = "Old Letters",
            Artist = "Quiet Keys",
            Genre = "Classical",
            Tags = ["nostalgia"],
            MoodAffinities =
            {
                ["nostalgic"] = 0.95,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "song-indie-1",
            Title = "Empty Streets",
            Artist = "Northbound",
            Genre = "Indie",
            Tags = ["lonely"],
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["intense"] = 0.3
            }
        },

        // Romantic
        new()
        {
            Id = "song-romantic-1",
            Title = "Moonlit Walk",
            Artist = "Velvet Sky",
            Genre = "Jazz",
            Tags = ["romantic"],
            MoodAffinities =
            {
                ["romantic"] = 0.95,
                ["calm"] = 0.6
            }
        },
        new()
        {
            Id = "song-romantic-2",
            Title = "You and Me",
            Artist = "Velvet Sky",
            Genre = "Soul",
            Tags = ["love"],
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["uplifting"] = 0.5
            }
        },

        // Müslüm Gürses block – strong melancholic / intense / nostalgic mapping
        new()
        {
            Id = "song-mg-1",
            Title = "Affet",
            Artist = "Müslüm Gürses",
            Genre = "Arabesk",
            Tags = ["pain", "melancholy"],
            MoodAffinities =
            {
                ["melancholic"] = 0.98,
                ["nostalgic"] = 0.9,
                ["intense"] = 0.6
            }
        },
        new()
        {
            Id = "song-mg-2",
            Title = "Hangimiz Sevmedik",
            Artist = "Müslüm Gürses",
            Genre = "Arabesk",
            Tags = ["love", "nostalgia"],
            MoodAffinities =
            {
                ["nostalgic"] = 0.97,
                ["romantic"] = 0.8,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "song-mg-3",
            Title = "İtirazım Var",
            Artist = "Müslüm Gürses",
            Genre = "Arabesk / Rock",
            Tags = ["rebel"],
            MoodAffinities =
            {
                ["rebellious"] = 0.95,
                ["intense"] = 0.9,
                ["melancholic"] = 0.5
            }
        },
        new()
        {
            Id = "song-mg-4",
            Title = "Paramparça",
            Artist = "Müslüm Gürses",
            Genre = "Arabesk Rock",
            Tags = ["heartbreak"],
            MoodAffinities =
            {
                ["melancholic"] = 0.96,
                ["intense"] = 0.7,
                ["nostalgic"] = 0.6
            }
        },
        new()
        {
            Id = "song-mg-5",
            Title = "Nilüfer",
            Artist = "Müslüm Gürses",
            Genre = "Cover",
            Tags = ["cover", "classic"],
            MoodAffinities =
            {
                ["nostalgic"] = 0.95,
                ["romantic"] = 0.7,
                ["melancholic"] = 0.6
            }
        },

        // Extra songs to exceed 30
        new()
        {
            Id = "song-extra-1",
            Title = "Night Runner",
            Artist = "Synthline",
            Genre = "Synthwave",
            Tags = ["retro"],
            MoodAffinities =
            {
                ["intense"] = 0.7,
                ["uplifting"] = 0.6,
                ["nostalgic"] = 0.7
            }
        },
        new()
        {
            Id = "song-extra-2",
            Title = "Ocean Breath",
            Artist = "Blue Horizon",
            Genre = "Ambient",
            Tags = ["sea"],
            MoodAffinities =
            {
                ["calm"] = 0.95,
                ["focused"] = 0.5
            }
        },
        new()
        {
            Id = "song-extra-3",
            Title = "City Lights",
            Artist = "Northbound",
            Genre = "Indie",
            Tags = ["night"],
            MoodAffinities =
            {
                ["uplifting"] = 0.7,
                ["nostalgic"] = 0.5
            }
        },
        new()
        {
            Id = "song-extra-4",
            Title = "Late Train",
            Artist = "Northbound",
            Genre = "Indie",
            Tags = ["travel"],
            MoodAffinities =
            {
                ["melancholic"] = 0.7,
                ["calm"] = 0.5
            }
        },
        new()
        {
            Id = "song-extra-5",
            Title = "Road Trip",
            Artist = "Aurora Days",
            Genre = "Pop Rock",
            Tags = ["road"],
            MoodAffinities =
            {
                ["uplifting"] = 0.9,
                ["rebellious"] = 0.4
            }
        },
        new()
        {
            Id = "song-extra-6",
            Title = "Study Flow",
            Artist = "NeuroBeats",
            Genre = "Lo-fi",
            Tags = ["study"],
            MoodAffinities =
            {
                ["focused"] = 0.92,
                ["calm"] = 0.7
            }
        },
        new()
        {
            Id = "song-extra-7",
            Title = "Last Dance",
            Artist = "Velvet Sky",
            Genre = "Soul",
            Tags = ["love"],
            MoodAffinities =
            {
                ["romantic"] = 0.93,
                ["melancholic"] = 0.5
            }
        },
        new()
        {
            Id = "song-extra-8",
            Title = "Broken Neon",
            Artist = "Street Lights",
            Genre = "Alt Rock",
            Tags = ["city"],
            MoodAffinities =
            {
                ["rebellious"] = 0.8,
                ["intense"] = 0.7
            }
        },
        new()
        {
            Id = "song-extra-9",
            Title = "First Snow",
            Artist = "Quiet Keys",
            Genre = "Classical",
            Tags = ["winter"],
            MoodAffinities =
            {
                ["nostalgic"] = 0.85,
                ["calm"] = 0.7
            }
        },
        new()
        {
            Id = "song-extra-10",
            Title = "Morning Run",
            Artist = "Iron Pulse",
            Genre = "Rock",
            Tags = ["sport"],
            MoodAffinities =
            {
                ["uplifting"] = 0.8,
                ["intense"] = 0.75
            }
        }
    ];

    public IReadOnlyList<Song> GetAll() => _songs;
}


