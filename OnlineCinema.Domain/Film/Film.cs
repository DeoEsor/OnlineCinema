using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
// ReSharper disable ClassNeverInstantiated.Global
#pragma warning disable CS8618

namespace OnlineCinema.Domain;

[Table("Film")]
public class Film
{
    [Key] public int Id { get; set; }

    [Required] public string FilmName { get; set; }

    public string Description { get; set; }

    public string? PosterSource { get; set; }

    public List<Actor> Cast { get; set; }
    
    public Genre Genres { get; set; }

    public float IMDbRaiting { get; set; }

    public float RottenTomatoesRaiting { get; set; }

    public DateTime ReleaseData { get; set; }

    public string Runtime { get; set; }

    public List<Director> Directors { get; set; }

    public List<Writer> Writers { get; set; }

    [Required] public string? MagnetLink { get; set; }
}