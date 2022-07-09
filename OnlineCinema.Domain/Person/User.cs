using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCinema.Domain.Core;

namespace OnlineCinema.Domain.Person;

public class User : IDataErrorInfo
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

    public string Country { get; set; }

    public Name Nickname { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string ImageSource { get; set; }

    public Email Email { get; set; }

    [Column(TypeName = "BINARY(64)")] public Password Password { get; set; }

    public string Error { get; }

    public string this[string columnName]
    {
        get
        {
            var error = columnName switch
            {
                "Age" => Age["Value"],
                "PersonalName" => PersonalName["FullName"],

                _ => string.Empty
            };
            return error;
        }
    }
}