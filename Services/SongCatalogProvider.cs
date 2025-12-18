using MyMvcProject.Models;

namespace MyMvcProject.Services;

public interface ISongCatalogProvider
{
    IReadOnlyList<Song> GetAll();
}

public class SongCatalogProvider : ISongCatalogProvider
{
    // In-memory seeded song catalog based on the provided genre/artist/song list.
    private readonly List<Song> _songs =
    [
        // 1. Classical → Contemplative / Deep Thinking
        new()
        {
            Id = "bach-air-on-the-g-string",
            Title = "Air on the G String",
            Artist = "Johann Sebastian Bach",
            Genre = "Classical",
            Tags = ["contemplative", "deep-thinking"],
            MoodAffinities =
            {
                ["focused"] = 0.9,
                ["calm"] = 0.9,
                ["melancholic"] = 0.4
            }
        },
        new()
        {
            Id = "bach-goldberg-variations",
            Title = "Goldberg Variations",
            Artist = "Johann Sebastian Bach",
            Genre = "Classical",
            Tags = ["contemplative"],
            MoodAffinities =
            {
                ["focused"] = 0.95,
                ["calm"] = 0.8
            }
        },
        new()
        {
            Id = "bach-cello-suite-no1",
            Title = "Cello Suite No.1",
            Artist = "Johann Sebastian Bach",
            Genre = "Classical",
            Tags = ["introspective"],
            MoodAffinities =
            {
                ["melancholic"] = 0.7,
                ["calm"] = 0.8,
                ["focused"] = 0.7
            }
        },
        new()
        {
            Id = "bach-brandenburg-3",
            Title = "Brandenburg Concerto No.3",
            Artist = "Johann Sebastian Bach",
            Genre = "Classical",
            Tags = ["energetic", "baroque"],
            MoodAffinities =
            {
                ["focused"] = 0.8,
                ["uplifting"] = 0.6
            }
        },
        new()
        {
            Id = "bach-toccata-fugue-d-minor",
            Title = "Toccata and Fugue in D Minor",
            Artist = "Johann Sebastian Bach",
            Genre = "Classical",
            Tags = ["dramatic"],
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["melancholic"] = 0.5
            }
        },

        // 2. Jazz → Sophisticated / Smooth (Miles Davis)
        new()
        {
            Id = "miles-so-what",
            Title = "So What",
            Artist = "Miles Davis",
            Genre = "Jazz",
            Tags = ["cool", "smooth"],
            MoodAffinities =
            {
                ["calm"] = 0.8,
                ["nostalgic"] = 0.6,
                ["romantic"] = 0.4
            }
        },
        new()
        {
            Id = "miles-blue-in-green",
            Title = "Blue in Green",
            Artist = "Miles Davis",
            Genre = "Jazz",
            Tags = ["late-night"],
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["calm"] = 0.7,
                ["nostalgic"] = 0.7
            }
        },
        new()
        {
            Id = "miles-freddie-freeloader",
            Title = "Freddie Freeloader",
            Artist = "Miles Davis",
            Genre = "Jazz",
            Tags = ["swing"],
            MoodAffinities =
            {
                ["calm"] = 0.7,
                ["uplifting"] = 0.6
            }
        },
        new()
        {
            Id = "miles-all-blues",
            Title = "All Blues",
            Artist = "Miles Davis",
            Genre = "Jazz",
            Tags = ["blues"],
            MoodAffinities =
            {
                ["nostalgic"] = 0.8,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "miles-flamenco-sketches",
            Title = "Flamenco Sketches",
            Artist = "Miles Davis",
            Genre = "Jazz",
            Tags = ["smooth"],
            MoodAffinities =
            {
                ["calm"] = 0.8,
                ["romantic"] = 0.5
            }
        },

        // 3. Rock → Energetic / Rebellious (Queen)
        new()
        {
            Id = "queen-bohemian-rhapsody",
            Title = "Bohemian Rhapsody",
            Artist = "Queen",
            Genre = "Rock",
            Tags = ["epic"],
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["rebellious"] = 0.8,
                ["uplifting"] = 0.6
            }
        },
        new()
        {
            Id = "queen-we-will-rock-you",
            Title = "We Will Rock You",
            Artist = "Queen",
            Genre = "Rock",
            Tags = ["anthem"],
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["rebellious"] = 0.9,
                ["uplifting"] = 0.7
            }
        },
        new()
        {
            Id = "queen-another-one-bites-the-dust",
            Title = "Another One Bites the Dust",
            Artist = "Queen",
            Genre = "Rock",
            Tags = ["groove"],
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["rebellious"] = 0.7
            }
        },
        new()
        {
            Id = "queen-dont-stop-me-now",
            Title = "Don’t Stop Me Now",
            Artist = "Queen",
            Genre = "Rock",
            Tags = ["high-energy"],
            MoodAffinities =
            {
                ["uplifting"] = 0.95,
                ["intense"] = 0.7,
                ["rebellious"] = 0.5
            }
        },
        new()
        {
            Id = "queen-killer-queen",
            Title = "Killer Queen",
            Artist = "Queen",
            Genre = "Rock",
            Tags = ["stylish"],
            MoodAffinities =
            {
                ["rebellious"] = 0.7,
                ["uplifting"] = 0.6
            }
        },

        // 4. Pop → Upbeat / Light-hearted (Tarkan)
        new()
        {
            Id = "tarkan-simarik",
            Title = "Şımarık",
            Artist = "Tarkan",
            Genre = "Pop",
            Tags = ["upbeat"],
            MoodAffinities =
            {
                ["uplifting"] = 0.95,
                ["romantic"] = 0.7
            }
        },
        new()
        {
            Id = "tarkan-kuzu-kuzu",
            Title = "Kuzu Kuzu",
            Artist = "Tarkan",
            Genre = "Pop",
            Tags = ["dance"],
            MoodAffinities =
            {
                ["uplifting"] = 0.9,
                ["romantic"] = 0.6
            }
        },
        new()
        {
            Id = "tarkan-dudu",
            Title = "Dudu",
            Artist = "Tarkan",
            Genre = "Pop",
            Tags = ["joyful"],
            MoodAffinities =
            {
                ["uplifting"] = 0.9,
                ["romantic"] = 0.6
            }
        },
        new()
        {
            Id = "tarkan-yolla",
            Title = "Yolla",
            Artist = "Tarkan",
            Genre = "Pop",
            Tags = ["energetic"],
            MoodAffinities =
            {
                ["uplifting"] = 0.9,
                ["rebellious"] = 0.4
            }
        },
        new()
        {
            Id = "tarkan-op",
            Title = "Öp",
            Artist = "Tarkan",
            Genre = "Pop",
            Tags = ["romantic"],
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["uplifting"] = 0.7
            }
        },

        // 5. Electronic / EDM → High Energy / Modern (Daft Punk)
        new()
        {
            Id = "daftpunk-one-more-time",
            Title = "One More Time",
            Artist = "Daft Punk",
            Genre = "Electronic",
            Tags = ["party"],
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["uplifting"] = 0.9
            }
        },
        new()
        {
            Id = "daftpunk-harder-better-faster-stronger",
            Title = "Harder Better Faster Stronger",
            Artist = "Daft Punk",
            Genre = "Electronic",
            Tags = ["modern"],
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["focused"] = 0.6
            }
        },
        new()
        {
            Id = "daftpunk-get-lucky",
            Title = "Get Lucky",
            Artist = "Daft Punk",
            Genre = "Electronic",
            Tags = ["funk"],
            MoodAffinities =
            {
                ["uplifting"] = 0.9,
                ["romantic"] = 0.5
            }
        },
        new()
        {
            Id = "daftpunk-around-the-world",
            Title = "Around the World",
            Artist = "Daft Punk",
            Genre = "Electronic",
            Tags = ["loop"],
            MoodAffinities =
            {
                ["intense"] = 0.7,
                ["uplifting"] = 0.8
            }
        },
        new()
        {
            Id = "daftpunk-digital-love",
            Title = "Digital Love",
            Artist = "Daft Punk",
            Genre = "Electronic",
            Tags = ["retro"],
            MoodAffinities =
            {
                ["romantic"] = 0.7,
                ["nostalgic"] = 0.6
            }
        },

        // 6. Lo-fi / Chill → Calm / Focused (Nujabes)
        new()
        {
            Id = "nujabes-feather",
            Title = "Feather",
            Artist = "Nujabes",
            Genre = "Lo-fi",
            Tags = ["chill"],
            MoodAffinities =
            {
                ["calm"] = 0.95,
                ["focused"] = 0.8
            }
        },
        new()
        {
            Id = "nujabes-aruarian-dance",
            Title = "Aruarian Dance",
            Artist = "Nujabes",
            Genre = "Lo-fi",
            Tags = ["relaxed"],
            MoodAffinities =
            {
                ["calm"] = 0.95,
                ["focused"] = 0.7,
                ["nostalgic"] = 0.5
            }
        },
        new()
        {
            Id = "nujabes-luvsic-3",
            Title = "Luv(sic) Part 3",
            Artist = "Nujabes",
            Genre = "Lo-fi",
            Tags = ["lyrical"],
            MoodAffinities =
            {
                ["calm"] = 0.9,
                ["melancholic"] = 0.6,
                ["romantic"] = 0.5
            }
        },
        new()
        {
            Id = "nujabes-horizon",
            Title = "Horizon",
            Artist = "Nujabes",
            Genre = "Lo-fi",
            Tags = ["study"],
            MoodAffinities =
            {
                ["focused"] = 0.9,
                ["calm"] = 0.8
            }
        },
        new()
        {
            Id = "nujabes-counting-stars",
            Title = "Counting Stars",
            Artist = "Nujabes",
            Genre = "Lo-fi",
            Tags = ["night"],
            MoodAffinities =
            {
                ["calm"] = 0.9,
                ["nostalgic"] = 0.6
            }
        },

        // 7. Heavy Metal → Intense / Dark (Metallica)
        new()
        {
            Id = "metallica-enter-sandman",
            Title = "Enter Sandman",
            Artist = "Metallica",
            Genre = "Metal",
            Tags = ["heavy"],
            MoodAffinities =
            {
                ["intense"] = 0.95,
                ["rebellious"] = 0.8
            }
        },
        new()
        {
            Id = "metallica-master-of-puppets",
            Title = "Master of Puppets",
            Artist = "Metallica",
            Genre = "Metal",
            Tags = ["dark"],
            MoodAffinities =
            {
                ["intense"] = 0.98,
                ["rebellious"] = 0.8,
                ["melancholic"] = 0.4
            }
        },
        new()
        {
            Id = "metallica-nothing-else-matters",
            Title = "Nothing Else Matters",
            Artist = "Metallica",
            Genre = "Metal",
            Tags = ["ballad"],
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["romantic"] = 0.5,
                ["intense"] = 0.6
            }
        },
        new()
        {
            Id = "metallica-sad-but-true",
            Title = "Sad But True",
            Artist = "Metallica",
            Genre = "Metal",
            Tags = ["heavy"],
            MoodAffinities =
            {
                ["intense"] = 0.95,
                ["melancholic"] = 0.5
            }
        },
        new()
        {
            Id = "metallica-the-unforgiven",
            Title = "The Unforgiven",
            Artist = "Metallica",
            Genre = "Metal",
            Tags = ["epic"],
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["intense"] = 0.7
            }
        },

        // 8. Indie / Alternative → Thoughtful / Melancholic (Radiohead)
        new()
        {
            Id = "radiohead-creep",
            Title = "Creep",
            Artist = "Radiohead",
            Genre = "Indie / Alternative",
            Tags = ["dark"],
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["intense"] = 0.6
            }
        },
        new()
        {
            Id = "radiohead-no-surprises",
            Title = "No Surprises",
            Artist = "Radiohead",
            Genre = "Indie / Alternative",
            Tags = ["sad"],
            MoodAffinities =
            {
                ["melancholic"] = 0.95,
                ["nostalgic"] = 0.8
            }
        },
        new()
        {
            Id = "radiohead-karma-police",
            Title = "Karma Police",
            Artist = "Radiohead",
            Genre = "Indie / Alternative",
            Tags = ["moody"],
            MoodAffinities =
            {
                ["melancholic"] = 0.85,
                ["intense"] = 0.5
            }
        },
        new()
        {
            Id = "radiohead-fake-plastic-trees",
            Title = "Fake Plastic Trees",
            Artist = "Radiohead",
            Genre = "Indie / Alternative",
            Tags = ["introspective"],
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["nostalgic"] = 0.7
            }
        },
        new()
        {
            Id = "radiohead-exit-music",
            Title = "Exit Music (For a Film)",
            Artist = "Radiohead",
            Genre = "Indie / Alternative",
            Tags = ["cinematic"],
            MoodAffinities =
            {
                ["melancholic"] = 0.95,
                ["intense"] = 0.6
            }
        },

        // 9. Hip-Hop / Rap → Confident / Storytelling (Ceza)
        new()
        {
            Id = "ceza-suspus",
            Title = "Suspus",
            Artist = "Ceza",
            Genre = "Hip-Hop / Rap",
            Tags = ["storytelling"],
            MoodAffinities =
            {
                ["rebellious"] = 0.9,
                ["intense"] = 0.85
            }
        },
        new()
        {
            Id = "ceza-neyim-var-ki",
            Title = "Neyim Var Ki",
            Artist = "Ceza",
            Genre = "Hip-Hop / Rap",
            Tags = ["confident"],
            MoodAffinities =
            {
                ["rebellious"] = 0.9,
                ["intense"] = 0.8,
                ["uplifting"] = 0.5
            }
        },
        new()
        {
            Id = "ceza-med-cezir",
            Title = "Med Cezir",
            Artist = "Ceza",
            Genre = "Hip-Hop / Rap",
            Tags = ["classic"],
            MoodAffinities =
            {
                ["rebellious"] = 0.85,
                ["intense"] = 0.8
            }
        },
        new()
        {
            Id = "ceza-yerli-plaka",
            Title = "Yerli Plaka",
            Artist = "Ceza",
            Genre = "Hip-Hop / Rap",
            Tags = ["urban"],
            MoodAffinities =
            {
                ["rebellious"] = 0.9,
                ["intense"] = 0.8
            }
        },
        new()
        {
            Id = "ceza-panorama-harem",
            Title = "Panorama Harem",
            Artist = "Ceza",
            Genre = "Hip-Hop / Rap",
            Tags = ["story"],
            MoodAffinities =
            {
                ["rebellious"] = 0.85,
                ["intense"] = 0.8
            }
        },

        // 10. R&B / Soul → Romantic / Emotional (Adele)
        new()
        {
            Id = "adele-someone-like-you",
            Title = "Someone Like You",
            Artist = "Adele",
            Genre = "R&B / Soul",
            Tags = ["heartbreak"],
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["melancholic"] = 0.9
            }
        },
        new()
        {
            Id = "adele-hello",
            Title = "Hello",
            Artist = "Adele",
            Genre = "R&B / Soul",
            Tags = ["emotional"],
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["romantic"] = 0.8
            }
        },
        new()
        {
            Id = "adele-rolling-in-the-deep",
            Title = "Rolling in the Deep",
            Artist = "Adele",
            Genre = "R&B / Soul",
            Tags = ["powerful"],
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["melancholic"] = 0.7,
                ["romantic"] = 0.6
            }
        },
        new()
        {
            Id = "adele-all-i-ask",
            Title = "All I Ask",
            Artist = "Adele",
            Genre = "R&B / Soul",
            Tags = ["ballad"],
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["romantic"] = 0.8
            }
        },
        new()
        {
            Id = "adele-when-we-were-young",
            Title = "When We Were Young",
            Artist = "Adele",
            Genre = "R&B / Soul",
            Tags = ["nostalgic"],
            MoodAffinities =
            {
                ["nostalgic"] = 0.9,
                ["melancholic"] = 0.8,
                ["romantic"] = 0.7
            }
        }
    ];

    public IReadOnlyList<Song> GetAll() => _songs;
}


