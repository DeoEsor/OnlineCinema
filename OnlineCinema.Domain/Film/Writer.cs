using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain;

[Table("Writers")]
public class Writer : IUpdatable<Writer>
{

    [Key] public int Id { get; set; }

    public string PersonalName { get; set; }

    public int Age { get; set; }

    public string Country { get; set; }

    public DateTime DateOfBirth { get; set; }
    public Writer Update(Writer updated)
    {
        if (Id != updated.Id)
            throw new ArgumentException(nameof(updated));
        PersonalName = updated.PersonalName;
        Age = updated.Age;
        Country = updated.Country;
        DateOfBirth = updated.DateOfBirth;
        return this;
    }
}