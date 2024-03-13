using Domain.Exceptions;

namespace Domain.ValueObjects.User;

public record FirstName
{
    public string Value { get; init; }

    public FirstName(string value)
    {
        if ( string.IsNullOrWhiteSpace(value) || value.Length < 2 || value.Length > 50)
        {
            throw new InvalidFirstNameException(value);
        }
        Value = value;
    }
    
    public static implicit operator FirstName(string value) => new(value);
    public static implicit operator string(FirstName value) => value.Value;
    public override string ToString() => Value;
}
