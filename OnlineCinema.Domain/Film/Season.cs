using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCinema.Domain;

[Table("Seasons")]
public class Season : IEqualityComparer<Season>, IComparable<Season>
{
    [Key] public int Id { get; set; }
    
    [Required] public int SeasonNumber { get; set; }

    [Required] public string SerialName { get; set; }
    
    [Required] public string SeasonName { get; set; }

    public string Description { get; set; }

    public string? PosterSource { get; set; }

    public List<Actor> Cast { get; set; }

    public float IMDbRaiting { get; set; }

    public float RottenTomatoesRaiting { get; set; }

    public DateTime ReleaseData { get; set; }

    public string Runtime { get; set; }

    public List<Director> Directors { get; set; }

    public List<Writer> Writers { get; set; }
    
    public List<Episode> Episodes { get; set; }

    public string? MagnetLink { get; set; }
    
    public bool Equals(Season? x, Season? y)
    {
        if (x == null || y == null)
            return false;

        return x.SeasonName == y.SeasonName
               && x.SeasonNumber == y.SeasonNumber;
    }

    public int GetHashCode(Season obj)
    {
        return string.Format($"{obj.Id}{obj.SeasonName}{obj.SeasonNumber}").GetHashCode();
    }

    public int CompareTo(Season? other)
    {
        if (other != null && SeasonName == other.SeasonName)
            return SeasonNumber.CompareTo(other.SeasonNumber);

        return -1;
    }
}