using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Core;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain;

[Table("Writers")]
public class Writer : IUpdatable<Writer>
{
    protected Writer(string country)
    {
        Country = country;
    }

    public Writer(PersonalName personalName, DateTime dateOfBirth, string country)
    {
        PersonalName = personalName ?? throw new ArgumentNullException(nameof(personalName));
        DateOfBirth = dateOfBirth;
        Country = country;
        var age = Age; // throwing InvalidArgument if DateOfBirth is invalid 
    }

    [Key] public int Id { get; }

    public PersonalName PersonalName { get; set; }

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