using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain;

[Table("Seasons")]
public class Season : IEqualityComparer<Season>, IComparable<Season>, IUpdatable<Season>
{
    [Key] public int Id { get; set; }
    
    [Required] public int SeasonNumber { get; set; }

    [Required] public string SerialName { get; set; }
    
    [Required] public string SeasonName { get; set; }

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

    public ICollection<Episode> Episodes { get; set; } = new List<Episode>();

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

    public Season Update(Season updated)
    {
        if (Id != updated.Id)
            throw new ArgumentException(nameof(updated));
        Description = updated.Description;
        PosterSource = updated.PosterSource;
        Cast = updated.Cast;
        IMDbRaiting = updated.IMDbRaiting;
        RottenTomatoesRaiting = updated.RottenTomatoesRaiting;
        Runtime = updated.Runtime;
        ReleaseData = updated.ReleaseData;
        Directors = updated.Directors;
        Writers = updated.Writers;
        Episodes = updated.Episodes;
        MagnetLink = updated.MagnetLink;
        return this;
    }
}