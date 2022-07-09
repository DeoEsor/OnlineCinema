using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCinema.Domain;

[Table("Film")]
public class Serial
{
    [Key] public int Id { get; set; }

    [Required] public string SerialName { get; set; }

    public string Description { get; set; }

    public string? PosterSource { get; set; }

    public List<Actor> Cast { get; set; }

    public float IMDbRaiting { get; set; }

    public float RottenTomatoesRaiting { get; set; }

    public DateTime ReleaseData { get; set; }

    public string AverageRuntime { get; set; }
    
    public List<Episode> Episodes { get; set; }

    public List<Director> Directors { get; set; }

    public List<Writter> Writers { get; set; }

    [Required] public string? MagnetLink { get; set; }
}