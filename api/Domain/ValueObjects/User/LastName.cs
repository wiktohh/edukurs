using Domain.Exceptions;

namespace Domain.ValueObjects.User;

public record LastName
{
    public string Value { get; }

    public LastName(string value)
    {
        if ( string.IsNullOrWhiteSpace(value) || value.Length < 2 || value.Length > 50)
        {
            throw new InvalidLastNameException(value);
        }
        Value = value;
    }
    
    public static implicit operator LastName(string value) => new(value);
    public static implicit operator string(LastName value) => value.Value;
    public override string ToString() => Value;
}