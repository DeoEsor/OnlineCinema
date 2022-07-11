using System.ComponentModel;

namespace OnlineCinema.BLL.Core;

public class PersonalName : IDataErrorInfo
{
    protected PersonalName()
    {
    }

    public PersonalName(Name firstName, Name lastName)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
    }

    public Name FirstName { get; }
    public Name LastName { get; }

    public string FullName => $"{FirstName} {LastName}";

    public string Error { get; }

    public string this[string columnName]
    {
        get
        {
            var error = string.Empty;

            switch (columnName)
            {
                case "FirstName":
                    if (!Name.IsValid(FirstName.Value))
                        error = "FirstName is not valid";
                    break;
                case "LastName":
                    if (!Name.IsValid(LastName.Value))
                        error = "LastName is not valid";
                    break;
                case "FullName":
                    if (!Name.IsValid(FirstName.Value) || !Name.IsValid(LastName.Value))
                        error = "FirstName or LastName is not valid";
                    break;
            }

            return error;
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is PersonalName personalName &&
               EqualityComparer<Name>.Default.Equals(FirstName, personalName.FirstName) &&
               EqualityComparer<Name>.Default.Equals(LastName, personalName.LastName);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName);
    }

    public override string ToString()
    {
        return FullName;
    }
}