using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OnlineCinema.Domain;

[Table("Films")]
public class Film
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string FilmName { get; set; }
    
    public string Description { get; set; }
    
    public string? PosterSource { get; set; }
    
    [Required]
    public string? TorrentLink { get; set; }
}