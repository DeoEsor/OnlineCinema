using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain;

[Table("Directors")]
public class Director : IUpdatable<Director>
{

    [Key]
    public int Id { get; set; }
    
    public string PersonalName { get; set; }
    
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