namespace MyMvcProject.Models;

public class UserPreferences
{
    public List<string> FavoriteGenres { get; set; } = new();
    public List<string> FavoriteMoods { get; set; } = new();
    public List<string> RecentInputs { get; set; } = new();
}

