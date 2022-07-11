using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain;

[Table("Actors")]
public class Actor : IUpdatable<Actor>
{

    [Key]
    public int Id { get; set; }

    public string PersonalName { get; set; }

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