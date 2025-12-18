using MyMvcProject.Models;

namespace MyMvcProject.Services;

public interface IRecommendationService
{
    Task<string> DetectMoodAsync(string musicInput);
    Task<List<Book>> GetRecommendationsAsync(string mood, int count = 10);
    Task<Book?> GetBookByIdAsync(int id);

    // New mood-mixing based recommendations (songs first, then books)
    List<SongResult> RecommendSongs(List<string> moodIds, int top = 5);
    BookResult? RecommendBook(List<string> moodIds);

    // New: book recommendation based on listening history (artists + songs)
    BookResult? RecommendBookFromListeningHistory(List<string> artistNames, List<string> songIds);
}

