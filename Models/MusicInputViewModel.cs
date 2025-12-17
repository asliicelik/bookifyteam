using System.ComponentModel.DataAnnotations;

namespace MyMvcProject.Models;

public class MusicInputViewModel
{
    [Display(Name = "Music Genre / Song / Artist / Mood")]
    [Required(ErrorMessage = "Please enter a music input")]
    public string Input { get; set; } = string.Empty;
    
    public string InputType { get; set; } = "auto"; // auto, genre, song, artist, mood
}

