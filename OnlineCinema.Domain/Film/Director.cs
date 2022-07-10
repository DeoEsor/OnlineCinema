using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Core;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain;

[Table("Directors")]
public class Director : IUpdatable<Director>
{
    protected Director(string country)
    {
        Country = country;
    }

    public Director(PersonalName personalName, int age, string country)
    {
        PersonalName = personalName ?? throw new ArgumentNullException(nameof(personalName));
        Age = age;
        Country = country; 
    }

    [Key]
    public int Id { get; }

    public PersonalName PersonalName { get; set; }

    public int Age;

    public string Country { get; set; }
    public Director Update(Director updated)
    {
        if (Id != updated.Id)
            throw new ArgumentException(nameof(updated));
        PersonalName = updated.PersonalName;
        Age = updated.Age;
        Country = updated.Country;
        return this;
    }
}