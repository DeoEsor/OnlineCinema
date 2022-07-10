using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Core;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain;

[Table("Actors")]
public class Actor : IUpdatable<Actor>
{
    protected Actor()
    { }

    public Actor(PersonalName personalName, DateTime dateOfBirth, string country)
    {
        PersonalName = personalName ?? throw new ArgumentNullException(nameof(personalName));
        Country = country;
        var age = Age; // throwing InvalidArgument if DateOfBirth is invalid 
    }

    [Key]
    public int Id { get; }

    public PersonalName PersonalName { get; set; }

    public int Age { get; set; }

    public string Country { get; set; }
    
    public Actor Update(Actor updated)
    {
        if (Id != updated.Id)
            throw new ArgumentException(nameof(updated));
        PersonalName = updated.PersonalName;
        Country = updated.Country;
        Age = updated.Age;
        return this;
    }
}