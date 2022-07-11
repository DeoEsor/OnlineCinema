using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.Domain.Extensions;

namespace OnlineCinema.BLL.Core;

[Owned]
public class Password : IDataErrorInfo
{
    private static readonly Regex ValidationRegex = new(
        @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,20})",
        RegexOptions.Singleline | RegexOptions.Compiled);

    protected Password()
    {
    }

    public Password([NotNull] string username, string value)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentNullException("Username is null", nameof(username));
        if (!IsValid(value))
            throw new ArgumentException("Name is not valid");

        Value = value;
        Username = username;
        Hashed = SaltedHash();
    }
    
    public Password(byte[] hash)
    {
        Hashed = hash ?? throw new ArgumentNullException(nameof(hash), "hash is null");
    }

    public string Value { get; }
    
    public byte[] Hashed { get; set; }
    
    public string Username { get; }
    
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

    public byte[] SaltedHash()
    {
        return Value.ToSaltedHash(Username);
    }

    public static bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && ValidationRegex.IsMatch(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Password other && other.Hashed == Hashed;
    }

    public override int GetHashCode()
    {
        return StringComparer.Ordinal.GetHashCode(Value);
    }
}