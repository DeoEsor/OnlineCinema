using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Core;

namespace OnlineCinema.Domain;

[Table("Directors")]
public class Director
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
}