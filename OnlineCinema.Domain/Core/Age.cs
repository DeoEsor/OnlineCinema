using System.ComponentModel;

namespace OnlineCinema.Domain.Core;

public class Age : IDataErrorInfo, IComparable<int>, IComparable<Age>, IComparable
{
    protected Age()
    {
    }

    public Age(int value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Age is not valid");

        Value = value;
    }

    public Age(DateTime value) : this(value.Year - DateTime.Now.Year)
    {
    }

    public int Value { get; }

    public string Error { get; } = null!;

    public string this[string columnName]
    {
        get
        {
            var error = string.Empty;
            switch (columnName)
            {
                case "Value":
                    if (!IsValid(Value))
                        error = "Age is not valid";
                    break;
            }

            return error;
        }
    }

    public static bool IsValid(int value)
    {
        return value is >= 10 and <= 120;
    }

    public override bool Equals(object? obj)
    {
        return obj is Age other && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is Age other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Age)}");
    }

    public int CompareTo(Age? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        var valueComparison = Value.CompareTo(other.Value);
        return valueComparison;
    }
    
    public int CompareTo(int other)
    {
        var valueComparison = Value.CompareTo(other);
        return valueComparison;
    }
}