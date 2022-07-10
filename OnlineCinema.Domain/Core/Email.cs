using System.ComponentModel;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace OnlineCinema.Domain.Core;

[Owned]
public class Email : IDataErrorInfo, IComparable<string>, IComparable<Email>, IComparable
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

    public int CompareTo(Email? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }
    
    public int CompareTo(string? other)
    {
        if (ReferenceEquals(Value, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return string.Compare(Value, other, StringComparison.Ordinal);
    }

    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is Email other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Email)}");
    }

    public static bool operator <(Email? left, Email? right)
    {
        return Comparer<Email>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(Email? left, Email? right)
    {
        return Comparer<Email>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(Email? left, Email? right)
    {
        return Comparer<Email>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(Email? left, Email? right)
    {
        return Comparer<Email>.Default.Compare(left, right) >= 0;
    }
}