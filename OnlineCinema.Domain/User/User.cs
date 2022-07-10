using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Core;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain.User;

public class User : IDataErrorInfo, IUpdatable<User>
{
    protected User()
    {
    }

    public User(PersonalName personalName, DateTime dateOfBirth)
    {
        Id = Guid.NewGuid();
        PersonalName = personalName ?? throw new ArgumentNullException(nameof(personalName));
        DateOfBirth = dateOfBirth;
        var age = Age; // throwing InvalidArgument if DateOfBirth is invalid 
    }

    public Guid Id { get; }

    public PersonalName PersonalName { get; set; }
    
    public Age Age => new(DateOfBirth);

    [MaxLength(20)]
    public string Country { get; set; }

    public Name Username { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string ImageSource { get; set; }

    public Email Email { get; set; }

    [Column(TypeName = "BINARY(64)")] 
    public Password Password { get; set; }

    [NotMapped]
    public string Error { get; }

    [NotMapped]
    public string this[string columnName]
    {
        get
        {
            var error = columnName switch
            {
                "Age" => Age["Value"],
                "PersonalName" => PersonalName["FullName"],
                "Email" => Email["Value"],
                _ => string.Empty
            };
            return error;
        }
    }

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