using System.ComponentModel.DataAnnotations;

namespace MyMvcProject.Models;

public class ArtistOptionViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}

public class SongOptionViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}

public class ListeningHistoryViewModel
{
    [Display(Name = "Artists")]
    public List<string> SelectedArtistIds { get; set; } = new();

    [Display(Name = "Songs")]
    public List<string> SelectedSongIds { get; set; } = new();

    public List<ArtistOptionViewModel> Artists { get; set; } = new();
    public List<SongOptionViewModel> Songs { get; set; } = new();

    public BookResult? BookResult { get; set; }
    public string? ValidationMessage { get; set; }
}


