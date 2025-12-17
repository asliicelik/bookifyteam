using MyMvcProject.Models;

namespace MyMvcProject.Services;

public interface IRecommendationService
{
    Task<string> DetectMoodAsync(string musicInput);
    Task<List<Book>> GetRecommendationsAsync(string mood, int count = 10);
    Task<Book?> GetBookByIdAsync(int id);
}

