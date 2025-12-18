using MyMvcProject.Models;

namespace MyMvcProject.Services;

public interface IBookCatalogProvider
{
    IReadOnlyList<MoodWeightedBook> GetAll();
}

public class BookCatalogProvider : IBookCatalogProvider
{
    // Independent from EF Book entity – this is a mood-weighted catalog for recommendations.
    // Seeded based on the provided 10 genre clusters (10 books each).
    private readonly List<MoodWeightedBook> _books =
    [
        // 1️⃣ Literary Fiction / Philosophy / Historical Fiction
        // (Classical – Contemplative / Deep Thinking)
        new()
        {
            Id = "classical-1",
            Title = "The Stranger",
            Author = "Albert Camus",
            Category = "Literary Fiction / Philosophy / Historical Fiction",
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["focused"] = 0.8,
                ["calm"] = 0.6
            }
        },
        new()
        {
            Id = "classical-2",
            Title = "Nausea",
            Author = "Jean-Paul Sartre",
            Category = "Literary Fiction / Philosophy / Historical Fiction",
            MoodAffinities =
            {
                ["melancholic"] = 0.95,
                ["focused"] = 0.8
            }
        },
        new()
        {
            Id = "classical-3",
            Title = "The Name of the Rose",
            Author = "Umberto Eco",
            Category = "Literary Fiction / Philosophy / Historical Fiction",
            MoodAffinities =
            {
                ["focused"] = 0.9,
                ["calm"] = 0.6,
                ["nostalgic"] = 0.5
            }
        },
        new()
        {
            Id = "classical-4",
            Title = "War and Peace",
            Author = "Leo Tolstoy",
            Category = "Literary Fiction / Philosophy / Historical Fiction",
            MoodAffinities =
            {
                ["focused"] = 0.9,
                ["nostalgic"] = 0.7,
                ["calm"] = 0.6
            }
        },
        new()
        {
            Id = "classical-5",
            Title = "The Brothers Karamazov",
            Author = "Fyodor Dostoevsky",
            Category = "Literary Fiction / Philosophy / Historical Fiction",
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["focused"] = 0.8
            }
        },
        new()
        {
            Id = "classical-6",
            Title = "Siddhartha",
            Author = "Hermann Hesse",
            Category = "Literary Fiction / Philosophy / Historical Fiction",
            MoodAffinities =
            {
                ["calm"] = 0.9,
                ["focused"] = 0.8
            }
        },
        new()
        {
            Id = "classical-7",
            Title = "The Plague",
            Author = "Albert Camus",
            Category = "Literary Fiction / Philosophy / Historical Fiction",
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["intense"] = 0.6
            }
        },
        new()
        {
            Id = "classical-8",
            Title = "The Book Thief",
            Author = "Markus Zusak",
            Category = "Literary Fiction / Philosophy / Historical Fiction",
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["nostalgic"] = 0.8
            }
        },
        new()
        {
            Id = "classical-9",
            Title = "The Old Man and the Sea",
            Author = "Ernest Hemingway",
            Category = "Literary Fiction / Philosophy / Historical Fiction",
            MoodAffinities =
            {
                ["calm"] = 0.9,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "classical-10",
            Title = "A Tale of Two Cities",
            Author = "Charles Dickens",
            Category = "Literary Fiction / Philosophy / Historical Fiction",
            MoodAffinities =
            {
                ["nostalgic"] = 0.8,
                ["focused"] = 0.7
            }
        },

        // 2️⃣ Mystery / Noir / Biography / Literary Fiction
        // (Jazz – Sophisticated / Smooth)
        new()
        {
            Id = "jazz-1",
            Title = "The Big Sleep",
            Author = "Raymond Chandler",
            Category = "Mystery / Noir / Biography / Literary Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.7,
                ["nostalgic"] = 0.7
            }
        },
        new()
        {
            Id = "jazz-2",
            Title = "In Cold Blood",
            Author = "Truman Capote",
            Category = "Mystery / Noir / Biography / Literary Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "jazz-3",
            Title = "The Talented Mr. Ripley",
            Author = "Patricia Highsmith",
            Category = "Mystery / Noir / Biography / Literary Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["melancholic"] = 0.5
            }
        },
        new()
        {
            Id = "jazz-4",
            Title = "Gone Girl",
            Author = "Gillian Flynn",
            Category = "Mystery / Noir / Biography / Literary Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["melancholic"] = 0.5
            }
        },
        new()
        {
            Id = "jazz-5",
            Title = "The Girl with the Dragon Tattoo",
            Author = "Stieg Larsson",
            Category = "Mystery / Noir / Biography / Literary Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["rebellious"] = 0.6
            }
        },
        new()
        {
            Id = "jazz-6",
            Title = "The Maltese Falcon",
            Author = "Dashiell Hammett",
            Category = "Mystery / Noir / Biography / Literary Fiction",
            MoodAffinities =
            {
                ["nostalgic"] = 0.8,
                ["intense"] = 0.6
            }
        },
        new()
        {
            Id = "jazz-7",
            Title = "Midnight in the Garden of Good and Evil",
            Author = "John Berendt",
            Category = "Mystery / Noir / Biography / Literary Fiction",
            MoodAffinities =
            {
                ["nostalgic"] = 0.7,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "jazz-8",
            Title = "The Shadow of the Wind",
            Author = "Carlos Ruiz Zafón",
            Category = "Mystery / Noir / Biography / Literary Fiction",
            MoodAffinities =
            {
                ["nostalgic"] = 0.9,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "jazz-9",
            Title = "The Curious Incident of the Dog in the Night-Time",
            Author = "Mark Haddon",
            Category = "Mystery / Noir / Biography / Literary Fiction",
            MoodAffinities =
            {
                ["focused"] = 0.8,
                ["uplifting"] = 0.5
            }
        },
        new()
        {
            Id = "jazz-10",
            Title = "Steve Jobs",
            Author = "Walter Isaacson",
            Category = "Mystery / Noir / Biography / Literary Fiction",
            MoodAffinities =
            {
                ["focused"] = 0.8,
                ["intense"] = 0.6
            }
        },

        // 3️⃣ Thriller / Action / Adventure / Dystopian
        // (Rock – Energetic / Rebellious)
        new()
        {
            Id = "rock-1",
            Title = "The Hunger Games",
            Author = "Suzanne Collins",
            Category = "Thriller / Action / Adventure / Dystopian",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["rebellious"] = 0.8
            }
        },
        new()
        {
            Id = "rock-2",
            Title = "Divergent",
            Author = "Veronica Roth",
            Category = "Thriller / Action / Adventure / Dystopian",
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["rebellious"] = 0.8
            }
        },
        new()
        {
            Id = "rock-3",
            Title = "1984",
            Author = "George Orwell",
            Category = "Thriller / Action / Adventure / Dystopian",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "rock-4",
            Title = "Brave New World",
            Author = "Aldous Huxley",
            Category = "Thriller / Action / Adventure / Dystopian",
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["rebellious"] = 0.7
            }
        },
        new()
        {
            Id = "rock-5",
            Title = "Fight Club",
            Author = "Chuck Palahniuk",
            Category = "Thriller / Action / Adventure / Dystopian",
            MoodAffinities =
            {
                ["rebellious"] = 0.95,
                ["intense"] = 0.9
            }
        },
        new()
        {
            Id = "rock-6",
            Title = "The Maze Runner",
            Author = "James Dashner",
            Category = "Thriller / Action / Adventure / Dystopian",
            MoodAffinities =
            {
                ["intense"] = 0.85,
                ["rebellious"] = 0.7
            }
        },
        new()
        {
            Id = "rock-7",
            Title = "World War Z",
            Author = "Max Brooks",
            Category = "Thriller / Action / Adventure / Dystopian",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "rock-8",
            Title = "The Road",
            Author = "Cormac McCarthy",
            Category = "Thriller / Action / Adventure / Dystopian",
            MoodAffinities =
            {
                ["melancholic"] = 0.95,
                ["intense"] = 0.8
            }
        },
        new()
        {
            Id = "rock-9",
            Title = "Ready Player One",
            Author = "Ernest Cline",
            Category = "Thriller / Action / Adventure / Dystopian",
            MoodAffinities =
            {
                ["uplifting"] = 0.8,
                ["intense"] = 0.7
            }
        },
        new()
        {
            Id = "rock-10",
            Title = "The Girl Who Played with Fire",
            Author = "Stieg Larsson",
            Category = "Thriller / Action / Adventure / Dystopian",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["rebellious"] = 0.7
            }
        },

        // 4️⃣ Romance / Contemporary Fiction / Light Mystery
        // (Pop – Upbeat / Light-hearted)
        new()
        {
            Id = "pop-1",
            Title = "Me Before You",
            Author = "Jojo Moyes",
            Category = "Romance / Contemporary Fiction / Light Mystery",
            MoodAffinities =
            {
                ["romantic"] = 0.95,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "pop-2",
            Title = "The Rosie Project",
            Author = "Graeme Simsion",
            Category = "Romance / Contemporary Fiction / Light Mystery",
            MoodAffinities =
            {
                ["romantic"] = 0.8,
                ["uplifting"] = 0.9
            }
        },
        new()
        {
            Id = "pop-3",
            Title = "Bridget Jones’s Diary",
            Author = "Helen Fielding",
            Category = "Romance / Contemporary Fiction / Light Mystery",
            MoodAffinities =
            {
                ["romantic"] = 0.8,
                ["uplifting"] = 0.9
            }
        },
        new()
        {
            Id = "pop-4",
            Title = "Love, Rosie",
            Author = "Cecelia Ahern",
            Category = "Romance / Contemporary Fiction / Light Mystery",
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["nostalgic"] = 0.7
            }
        },
        new()
        {
            Id = "pop-5",
            Title = "Eleanor Oliphant Is Completely Fine",
            Author = "Gail Honeyman",
            Category = "Romance / Contemporary Fiction / Light Mystery",
            MoodAffinities =
            {
                ["uplifting"] = 0.8,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "pop-6",
            Title = "The Flatshare",
            Author = "Beth O’Leary",
            Category = "Romance / Contemporary Fiction / Light Mystery",
            MoodAffinities =
            {
                ["romantic"] = 0.85,
                ["uplifting"] = 0.9
            }
        },
        new()
        {
            Id = "pop-7",
            Title = "One Day",
            Author = "David Nicholls",
            Category = "Romance / Contemporary Fiction / Light Mystery",
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "pop-8",
            Title = "The Time Traveler’s Wife",
            Author = "Audrey Niffenegger",
            Category = "Romance / Contemporary Fiction / Light Mystery",
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "pop-9",
            Title = "Where Rainbows End",
            Author = "Cecelia Ahern",
            Category = "Romance / Contemporary Fiction / Light Mystery",
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["nostalgic"] = 0.8
            }
        },
        new()
        {
            Id = "pop-10",
            Title = "Crazy Rich Asians",
            Author = "Kevin Kwan",
            Category = "Romance / Contemporary Fiction / Light Mystery",
            MoodAffinities =
            {
                ["uplifting"] = 0.95,
                ["romantic"] = 0.7
            }
        },

        // 5️⃣ Science Fiction / Cyberpunk / Graphic Novels
        // (Electronic / EDM – High Energy / Modern)
        new()
        {
            Id = "edm-1",
            Title = "Neuromancer",
            Author = "William Gibson",
            Category = "Science Fiction / Cyberpunk / Graphic Novels",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["rebellious"] = 0.8
            }
        },
        new()
        {
            Id = "edm-2",
            Title = "Snow Crash",
            Author = "Neal Stephenson",
            Category = "Science Fiction / Cyberpunk / Graphic Novels",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["rebellious"] = 0.8
            }
        },
        new()
        {
            Id = "edm-3",
            Title = "Do Androids Dream of Electric Sheep?",
            Author = "Philip K. Dick",
            Category = "Science Fiction / Cyberpunk / Graphic Novels",
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "edm-4",
            Title = "Altered Carbon",
            Author = "Richard K. Morgan",
            Category = "Science Fiction / Cyberpunk / Graphic Novels",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["rebellious"] = 0.7
            }
        },
        new()
        {
            Id = "edm-5",
            Title = "Ready Player Two",
            Author = "Ernest Cline",
            Category = "Science Fiction / Cyberpunk / Graphic Novels",
            MoodAffinities =
            {
                ["uplifting"] = 0.8,
                ["intense"] = 0.7
            }
        },
        new()
        {
            Id = "edm-6",
            Title = "The Martian",
            Author = "Andy Weir",
            Category = "Science Fiction / Cyberpunk / Graphic Novels",
            MoodAffinities =
            {
                ["focused"] = 0.8,
                ["uplifting"] = 0.7
            }
        },
        new()
        {
            Id = "edm-7",
            Title = "Dune",
            Author = "Frank Herbert",
            Category = "Science Fiction / Cyberpunk / Graphic Novels",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "edm-8",
            Title = "Akira (Vol. 1)",
            Author = "Katsuhiro Otomo",
            Category = "Science Fiction / Cyberpunk / Graphic Novels",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["rebellious"] = 0.8
            }
        },
        new()
        {
            Id = "edm-9",
            Title = "Watchmen",
            Author = "Alan Moore",
            Category = "Science Fiction / Cyberpunk / Graphic Novels",
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "edm-10",
            Title = "Ghost in the Shell",
            Author = "Masamune Shirow",
            Category = "Science Fiction / Cyberpunk / Graphic Novels",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["rebellious"] = 0.8
            }
        },

        // 6️⃣ Self-Help / Poetry / Short Stories
        // (Lo-fi / Chill – Calm / Focused)
        new()
        {
            Id = "lofi-1",
            Title = "The Alchemist",
            Author = "Paulo Coelho",
            Category = "Self-Help / Poetry / Short Stories",
            MoodAffinities =
            {
                ["uplifting"] = 0.9,
                ["calm"] = 0.8
            }
        },
        new()
        {
            Id = "lofi-2",
            Title = "Milk and Honey",
            Author = "Rupi Kaur",
            Category = "Self-Help / Poetry / Short Stories",
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["romantic"] = 0.7
            }
        },
        new()
        {
            Id = "lofi-3",
            Title = "The Little Prince",
            Author = "Antoine de Saint-Exupéry",
            Category = "Self-Help / Poetry / Short Stories",
            MoodAffinities =
            {
                ["nostalgic"] = 0.9,
                ["calm"] = 0.8
            }
        },
        new()
        {
            Id = "lofi-4",
            Title = "Atomic Habits",
            Author = "James Clear",
            Category = "Self-Help / Poetry / Short Stories",
            MoodAffinities =
            {
                ["focused"] = 0.95,
                ["uplifting"] = 0.8
            }
        },
        new()
        {
            Id = "lofi-5",
            Title = "The Power of Now",
            Author = "Eckhart Tolle",
            Category = "Self-Help / Poetry / Short Stories",
            MoodAffinities =
            {
                ["calm"] = 0.95,
                ["focused"] = 0.8
            }
        },
        new()
        {
            Id = "lofi-6",
            Title = "Man’s Search for Meaning",
            Author = "Viktor E. Frankl",
            Category = "Self-Help / Poetry / Short Stories",
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["uplifting"] = 0.7
            }
        },
        new()
        {
            Id = "lofi-7",
            Title = "Tuesdays with Morrie",
            Author = "Mitch Albom",
            Category = "Self-Help / Poetry / Short Stories",
            MoodAffinities =
            {
                ["nostalgic"] = 0.8,
                ["uplifting"] = 0.8
            }
        },
        new()
        {
            Id = "lofi-8",
            Title = "Calm",
            Author = "Michael Acton Smith",
            Category = "Self-Help / Poetry / Short Stories",
            MoodAffinities =
            {
                ["calm"] = 0.95
            }
        },
        new()
        {
            Id = "lofi-9",
            Title = "The Four Agreements",
            Author = "Don Miguel Ruiz",
            Category = "Self-Help / Poetry / Short Stories",
            MoodAffinities =
            {
                ["calm"] = 0.8,
                ["uplifting"] = 0.7
            }
        },
        new()
        {
            Id = "lofi-10",
            Title = "Interpreter of Maladies",
            Author = "Jhumpa Lahiri",
            Category = "Self-Help / Poetry / Short Stories",
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["nostalgic"] = 0.8
            }
        },

        // 7️⃣ Horror / Dark Fantasy / Dystopian Fiction
        // (Heavy Metal – Intense / Dark)
        new()
        {
            Id = "metal-1",
            Title = "It",
            Author = "Stephen King",
            Category = "Horror / Dark Fantasy / Dystopian Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.95,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "metal-2",
            Title = "The Shining",
            Author = "Stephen King",
            Category = "Horror / Dark Fantasy / Dystopian Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.95
            }
        },
        new()
        {
            Id = "metal-3",
            Title = "Dracula",
            Author = "Bram Stoker",
            Category = "Horror / Dark Fantasy / Dystopian Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["nostalgic"] = 0.7
            }
        },
        new()
        {
            Id = "metal-4",
            Title = "The Haunting of Hill House",
            Author = "Shirley Jackson",
            Category = "Horror / Dark Fantasy / Dystopian Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "metal-5",
            Title = "The Stand",
            Author = "Stephen King",
            Category = "Horror / Dark Fantasy / Dystopian Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "metal-6",
            Title = "American Gods",
            Author = "Neil Gaiman",
            Category = "Horror / Dark Fantasy / Dystopian Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "metal-7",
            Title = "The Witcher: The Last Wish",
            Author = "Andrzej Sapkowski",
            Category = "Horror / Dark Fantasy / Dystopian Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["rebellious"] = 0.6
            }
        },
        new()
        {
            Id = "metal-8",
            Title = "Bird Box",
            Author = "Josh Malerman",
            Category = "Horror / Dark Fantasy / Dystopian Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["melancholic"] = 0.6
            }
        },
        new()
        {
            Id = "metal-9",
            Title = "The Handmaid’s Tale",
            Author = "Margaret Atwood",
            Category = "Horror / Dark Fantasy / Dystopian Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["melancholic"] = 0.8
            }
        },
        new()
        {
            Id = "metal-10",
            Title = "House of Leaves",
            Author = "Mark Z. Danielewski",
            Category = "Horror / Dark Fantasy / Dystopian Fiction",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["melancholic"] = 0.8
            }
        },

        // 8️⃣ Literary Fiction / Indie Press / Magical Realism
        // (Indie / Alternative – Thoughtful / Melancholic)
        new()
        {
            Id = "indie-1",
            Title = "Norwegian Wood",
            Author = "Haruki Murakami",
            Category = "Literary Fiction / Indie Press / Magical Realism",
            MoodAffinities =
            {
                ["melancholic"] = 0.95,
                ["nostalgic"] = 0.8
            }
        },
        new()
        {
            Id = "indie-2",
            Title = "Kafka on the Shore",
            Author = "Haruki Murakami",
            Category = "Literary Fiction / Indie Press / Magical Realism",
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["intense"] = 0.5
            }
        },
        new()
        {
            Id = "indie-3",
            Title = "The Perks of Being a Wallflower",
            Author = "Stephen Chbosky",
            Category = "Literary Fiction / Indie Press / Magical Realism",
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["uplifting"] = 0.6
            }
        },
        new()
        {
            Id = "indie-4",
            Title = "The Bell Jar",
            Author = "Sylvia Plath",
            Category = "Literary Fiction / Indie Press / Magical Realism",
            MoodAffinities =
            {
                ["melancholic"] = 0.98
            }
        },
        new()
        {
            Id = "indie-5",
            Title = "Never Let Me Go",
            Author = "Kazuo Ishiguro",
            Category = "Literary Fiction / Indie Press / Magical Realism",
            MoodAffinities =
            {
                ["melancholic"] = 0.95,
                ["nostalgic"] = 0.7
            }
        },
        new()
        {
            Id = "indie-6",
            Title = "One Hundred Years of Solitude",
            Author = "Gabriel García Márquez",
            Category = "Literary Fiction / Indie Press / Magical Realism",
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["nostalgic"] = 0.8
            }
        },
        new()
        {
            Id = "indie-7",
            Title = "The Ocean at the End of the Lane",
            Author = "Neil Gaiman",
            Category = "Literary Fiction / Indie Press / Magical Realism",
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["intense"] = 0.5
            }
        },
        new()
        {
            Id = "indie-8",
            Title = "The Virgin Suicides",
            Author = "Jeffrey Eugenides",
            Category = "Literary Fiction / Indie Press / Magical Realism",
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["nostalgic"] = 0.7
            }
        },
        new()
        {
            Id = "indie-9",
            Title = "Atonement",
            Author = "Ian McEwan",
            Category = "Literary Fiction / Indie Press / Magical Realism",
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["nostalgic"] = 0.7
            }
        },
        new()
        {
            Id = "indie-10",
            Title = "The Goldfinch",
            Author = "Donna Tartt",
            Category = "Literary Fiction / Indie Press / Magical Realism",
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["intense"] = 0.6
            }
        },

        // 9️⃣ Urban Fiction / Biography / Social Commentary
        // (Hip-Hop / Rap – Confident / Storytelling)
        new()
        {
            Id = "hiphop-1",
            Title = "The Autobiography of Malcolm X",
            Author = "Malcolm X",
            Category = "Urban Fiction / Biography / Social Commentary",
            MoodAffinities =
            {
                ["rebellious"] = 0.95,
                ["intense"] = 0.8
            }
        },
        new()
        {
            Id = "hiphop-2",
            Title = "Between the World and Me",
            Author = "Ta-Nehisi Coates",
            Category = "Urban Fiction / Biography / Social Commentary",
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["rebellious"] = 0.8
            }
        },
        new()
        {
            Id = "hiphop-3",
            Title = "Born a Crime",
            Author = "Trevor Noah",
            Category = "Urban Fiction / Biography / Social Commentary",
            MoodAffinities =
            {
                ["uplifting"] = 0.8,
                ["rebellious"] = 0.7
            }
        },
        new()
        {
            Id = "hiphop-4",
            Title = "American Dirt",
            Author = "Jeanine Cummins",
            Category = "Urban Fiction / Biography / Social Commentary",
            MoodAffinities =
            {
                ["intense"] = 0.9,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "hiphop-5",
            Title = "The Hate U Give",
            Author = "Angie Thomas",
            Category = "Urban Fiction / Biography / Social Commentary",
            MoodAffinities =
            {
                ["rebellious"] = 0.95,
                ["intense"] = 0.8
            }
        },
        new()
        {
            Id = "hiphop-6",
            Title = "The Brief Wondrous Life of Oscar Wao",
            Author = "Junot Díaz",
            Category = "Urban Fiction / Biography / Social Commentary",
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["rebellious"] = 0.7
            }
        },
        new()
        {
            Id = "hiphop-7",
            Title = "Hillbilly Elegy",
            Author = "J.D. Vance",
            Category = "Urban Fiction / Biography / Social Commentary",
            MoodAffinities =
            {
                ["melancholic"] = 0.7,
                ["intense"] = 0.6
            }
        },
        new()
        {
            Id = "hiphop-8",
            Title = "On the Road",
            Author = "Jack Kerouac",
            Category = "Urban Fiction / Biography / Social Commentary",
            MoodAffinities =
            {
                ["rebellious"] = 0.8,
                ["uplifting"] = 0.6
            }
        },
        new()
        {
            Id = "hiphop-9",
            Title = "Just Mercy",
            Author = "Bryan Stevenson",
            Category = "Urban Fiction / Biography / Social Commentary",
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["rebellious"] = 0.7
            }
        },
        new()
        {
            Id = "hiphop-10",
            Title = "Long Way Down",
            Author = "Jason Reynolds",
            Category = "Urban Fiction / Biography / Social Commentary",
            MoodAffinities =
            {
                ["intense"] = 0.8,
                ["melancholic"] = 0.7
            }
        },

        // 🔟 Romance / Literary Fiction / Poetry
        // (R&B / Soul – Romantic / Emotional)
        new()
        {
            Id = "rnb-1",
            Title = "Pride and Prejudice",
            Author = "Jane Austen",
            Category = "Romance / Literary Fiction / Poetry",
            MoodAffinities =
            {
                ["romantic"] = 0.95,
                ["uplifting"] = 0.7
            }
        },
        new()
        {
            Id = "rnb-2",
            Title = "Wuthering Heights",
            Author = "Emily Brontë",
            Category = "Romance / Literary Fiction / Poetry",
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["melancholic"] = 0.9
            }
        },
        new()
        {
            Id = "rnb-3",
            Title = "The Notebook",
            Author = "Nicholas Sparks",
            Category = "Romance / Literary Fiction / Poetry",
            MoodAffinities =
            {
                ["romantic"] = 0.95,
                ["melancholic"] = 0.7
            }
        },
        new()
        {
            Id = "rnb-4",
            Title = "Call Me by Your Name",
            Author = "André Aciman",
            Category = "Romance / Literary Fiction / Poetry",
            MoodAffinities =
            {
                ["romantic"] = 0.95,
                ["nostalgic"] = 0.8
            }
        },
        new()
        {
            Id = "rnb-5",
            Title = "Normal People",
            Author = "Sally Rooney",
            Category = "Romance / Literary Fiction / Poetry",
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["melancholic"] = 0.8
            }
        },
        new()
        {
            Id = "rnb-6",
            Title = "Love Letters to the Dead",
            Author = "Ava Dellaira",
            Category = "Romance / Literary Fiction / Poetry",
            MoodAffinities =
            {
                ["melancholic"] = 0.9,
                ["romantic"] = 0.8
            }
        },
        new()
        {
            Id = "rnb-7",
            Title = "The Song of Achilles",
            Author = "Madeline Miller",
            Category = "Romance / Literary Fiction / Poetry",
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["melancholic"] = 0.8
            }
        },
        new()
        {
            Id = "rnb-8",
            Title = "Jane Eyre",
            Author = "Charlotte Brontë",
            Category = "Romance / Literary Fiction / Poetry",
            MoodAffinities =
            {
                ["romantic"] = 0.9,
                ["melancholic"] = 0.8
            }
        },
        new()
        {
            Id = "rnb-9",
            Title = "The Time of Our Singing",
            Author = "Richard Powers",
            Category = "Romance / Literary Fiction / Poetry",
            MoodAffinities =
            {
                ["melancholic"] = 0.8,
                ["romantic"] = 0.8
            }
        },
        new()
        {
            Id = "rnb-10",
            Title = "Leaves of Grass",
            Author = "Walt Whitman",
            Category = "Romance / Literary Fiction / Poetry",
            MoodAffinities =
            {
                ["romantic"] = 0.8,
                ["uplifting"] = 0.7
            }
        }
    ];

    public IReadOnlyList<MoodWeightedBook> GetAll() => _books;
}


