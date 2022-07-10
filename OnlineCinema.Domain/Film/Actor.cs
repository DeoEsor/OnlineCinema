using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Core;

namespace OnlineCinema.Domain;

[Table("Actors")]
public class Actor
{
    protected Actor()
    { }

    public Actor(PersonalName personalName, DateTime dateOfBirth, string country)
    {
        PersonalName = personalName ?? throw new ArgumentNullException(nameof(personalName));
        DateOfBirth = dateOfBirth;
        Country = country;
        var age = Age; // throwing InvalidArgument if DateOfBirth is invalid 
    }

    [Key]
    public int Id { get; }

    public PersonalName PersonalName { get; set; }

    public Age Age => new(DateOfBirth);

    public string Country { get; set; }

    public DateTime DateOfBirth { get; set; }
}