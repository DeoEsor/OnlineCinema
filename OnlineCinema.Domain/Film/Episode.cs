using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain;

[Table("Episodes")]
public class Episode : IUpdatable<Episode>
{
    [Key] public int Id { get; set; }
    
    [Required] public int EpisodeNumber { get; set; }
    
    [Required] public int SeasonNumber { get; set; }

    [Required] public string SerialName { get; set; }
    
    [Required] public string EpisodeName { get; set; }

    public string Description { get; set; }

    public string? PosterSource { get; set; }

    public ICollection<Actor> Cast { get; set; } = new List<Actor>();

    public float IMDbRaiting { get; set; }

    public float RottenTomatoesRaiting { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime ReleaseData { get; set; }

    public string Runtime { get; set; }

    public ICollection<Director> Directors { get; set; } = new List<Director>();

    public ICollection<Writer> Writers { get; set; } = new List<Writer>();

    public string? MagnetLink { get; set; }
    public Episode Update(Episode updated)
    {
        if (Id != updated.Id)
            throw new ArgumentException(nameof(updated));
        Description = updated.Description;
        PosterSource = updated.PosterSource;
        Cast = updated.Cast;
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