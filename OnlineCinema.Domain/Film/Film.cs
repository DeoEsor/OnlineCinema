using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Interfaces;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
// ReSharper disable ClassNeverInstantiated.Global
#pragma warning disable CS8618

namespace OnlineCinema.Domain;

[Table("Films")]
public class Film : IUpdatable<Film>
{
    [Key] public int Id { get; set; }

    [Required] public string FilmName { get; set; }

    public string Description { get; set; }

    public string? PosterSource { get; set; }

    public ICollection<Actor> Cast { get; set; } = new List<Actor>();
    
    public Genre Genres { get; set; }

    public float IMDbRaiting { get; set; }

    public float RottenTomatoesRaiting { get; set; }
    
    [Column(TypeName = "datetime2")]
    public DateTime ReleaseData { get; set; }

    public string Runtime { get; set; }

    public ICollection<Director> Directors { get; set; } = new List<Director>();

    public ICollection<Writer> Writers { get; set; } = new List<Writer>();

    public string? MagnetLink { get; set; }
    public Film Update(Film updated)
    {
        if (Id != updated.Id)
            throw new ArgumentException(nameof(updated));
        Description = updated.Description;
        PosterSource = updated.PosterSource;
        Cast = updated.Cast;
        Genres = updated.Genres;
        IMDbRaiting = updated.IMDbRaiting;
        RottenTomatoesRaiting = updated.RottenTomatoesRaiting;
        ReleaseData = updated.ReleaseData;
        Runtime = updated.Runtime;
        Directors = updated.Directors;
        Writers = updated.Writers;
        MagnetLink = updated.MagnetLink;
        return this;
    }
}