using System.Text.Json;

namespace MyMvcProject.Models;

public class Mood
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Store as JSON string in database
    public string MusicKeywordsJson { get; set; } = "[]";
    public string MusicGenresJson { get; set; } = "[]";
    
    // Properties for easy access
    [System.Text.Json.Serialization.JsonIgnore]
    public List<string> MusicKeywords
    {
        get => string.IsNullOrEmpty(MusicKeywordsJson) 
            ? new List<string>() 
            : JsonSerializer.Deserialize<List<string>>(MusicKeywordsJson) ?? new List<string>();
        set => MusicKeywordsJson = JsonSerializer.Serialize(value);
    }
    
    [System.Text.Json.Serialization.JsonIgnore]
    public List<string> MusicGenres
    {
        get => string.IsNullOrEmpty(MusicGenresJson) 
            ? new List<string>() 
            : JsonSerializer.Deserialize<List<string>>(MusicGenresJson) ?? new List<string>();
        set => MusicGenresJson = JsonSerializer.Serialize(value);
    }
}

