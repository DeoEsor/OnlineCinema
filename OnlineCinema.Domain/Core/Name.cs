using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace OnlineCinema.Domain.Core;

[Owned]
public class Name : IDataErrorInfo, IComparable<string>, IComparable<Name>, IComparable
{
    private static readonly Regex ValidationRegex = new(
        @"^[\p{L}\p{M}\p{N}]{1,20}\z",
        RegexOptions.Singleline | RegexOptions.Compiled);

    protected Name()
    {
    }

    public Name(string value)
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
                        error = "Name is not valid";
                    break;
            }

            return error;
        }
    }

    public static bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && ValidationRegex.IsMatch(value);
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

    public int CompareTo(Name? other)
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
        return obj is Name other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Name)}");
    }

    public static bool operator <(Name? left, Name? right)
    {
        return Comparer<Name>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(Name? left, Name? right)
    {
        return Comparer<Name>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(Name? left, Name? right)
    {
        return Comparer<Name>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(Name? left, Name? right)
    {
        return Comparer<Name>.Default.Compare(left, right) >= 0;
    }
}