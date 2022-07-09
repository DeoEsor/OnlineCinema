using System.ComponentModel;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace OnlineCinema.Domain.Core;

[Owned]
public class Email : IDataErrorInfo
{
    protected Email()
    {
    }

    public Email(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Name is not valid");

        Value = value;
    }

    public string Value { get; }

    public string Error { get; }

    public string this[string columnName]
    {
        get
        {
            var error = string.Empty;

            switch (columnName)
            {
                case "Value":
                    if (!IsValid(Value))
                        error = "Email is not valid";
                    break;
            }

            return error;
        }
    }

    public bool IsValid(string emailAddress)
    {
        try
        {
            var m = new MailAddress(emailAddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }


    public override bool Equals(object? obj)
    {
        return obj is Name other &&
               StringComparer.Ordinal.Equals(Value, other.Value);
    }

    public override int GetHashCode()
    {
        return StringComparer.Ordinal.GetHashCode(Value);
    }
}