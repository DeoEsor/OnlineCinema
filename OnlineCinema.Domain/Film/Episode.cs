using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCinema.Domain;

[Table("Episodes")]
public class Episode
{
    [Key] public int Id { get; set; }
    
    [Required] public int EpisodeNumber { get; set; }
    
    [Required] public int SeasonNumber { get; set; }

    [Required] public string SerialName { get; set; }
    
    [Required] public string EpisodeName { get; set; }

    public string Description { get; set; }

    public string? PosterSource { get; set; }

    public List<Actor> Cast { get; set; }

    public float IMDbRaiting { get; set; }

    public float RottenTomatoesRaiting { get; set; }

    public DateTime ReleaseData { get; set; }

    public string Runtime { get; set; }

    public List<Director> Directors { get; set; }

    public List<Writter> Writers { get; set; }

    public string? MagnetLink { get; set; }
}