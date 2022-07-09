using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.Domain.Extensions;

namespace OnlineCinema.Domain.Core;

[Owned]
public class Password : IDataErrorInfo
{
    private static readonly Regex ValidationRegex = new(
        @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,20})",
        RegexOptions.Singleline | RegexOptions.Compiled);

    protected Password()
    {
    }

    public Password(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Name is not valid");

        Value = value;
    }

    [NotMapped] public string Value { get; }

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
                        error = "Password is not valid";
                    break;
            }

            return error;
        }
    }

    public byte[] HashValue(string username)
    {
        return Value.ToSaltedHash(username);
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