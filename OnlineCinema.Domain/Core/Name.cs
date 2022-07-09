using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace OnlineCinema.Domain.Core;

[Owned]
public class Name : IDataErrorInfo
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
}