using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain.User;

[Table("Users")]
public class User :  IUpdatable<User>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; }

    public string PersonalName { get; set; }

    public string? Country { get; set; }

    
    public string Username { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime DateOfBirth { get; set; }

    public string? ImageSource { get; set; }

    public string Email { get; set; }

    [Column(TypeName = "BINARY(64)")] 
    public byte[] Password { get; set; }
    
    public User Update(User updated)
    {
        if (Id != updated.Id)
            throw new ArgumentException(nameof(updated));
        PersonalName = updated.PersonalName;
        Country = updated.Country;
        DateOfBirth = updated.DateOfBirth;
        ImageSource = updated.ImageSource;
        Email = updated.Email;
        Password = updated.Password;
        return this;
    }
}