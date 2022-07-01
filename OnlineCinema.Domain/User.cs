using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OnlineCinema.Domain;

[Table("Users")]
public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Username { get; set; }
    
    [MaxLength(50), Required]
    public byte[] Password { get; set; } // I know that's bad thing, but bruh

    public bool IsOnline { get; set; }
    
    public string? ImageSource { get; set; }
    
    public string? Status { get; set; }
}