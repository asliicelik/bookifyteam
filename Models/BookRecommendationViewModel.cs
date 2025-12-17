namespace MyMvcProject.Models;

public class BookRecommendationViewModel
{
    public string DetectedMood { get; set; } = string.Empty;
    public string InputText { get; set; } = string.Empty;
    public List<Book> RecommendedBooks { get; set; } = new();
    public int TotalCount { get; set; }
}

