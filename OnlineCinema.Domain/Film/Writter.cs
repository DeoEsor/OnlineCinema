using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Core;

namespace OnlineCinema.Domain;

[Table("Writters")]
public class Writter
{
    protected Writter(string country)
    {
        Country = country;
    }

    public Writter(PersonalName personalName, DateTime dateOfBirth, string country)
    {
        Id = Guid.NewGuid();
        PersonalName = personalName ?? throw new ArgumentNullException(nameof(personalName));
        DateOfBirth = dateOfBirth;
        Country = country;
        var age = Age; // throwing InvalidArgument if DateOfBirth is invalid 
    }

    public Guid Id { get; }

    public PersonalName PersonalName { get; set; }

    public Age Age => new(DateOfBirth);

    public string Country { get; set; }

    public DateTime DateOfBirth { get; set; }
}