using Domain.Exceptions;

namespace Domain.ValueObjects.User;

public record Password
{
    public string Value { get; init; }
    
    public Password(string value)
    {
        if ( string.IsNullOrWhiteSpace(value) || value.Length < 4 || value.Length > 200)
        {
            throw new InvalidPasswordException();
        }
        Value = value;
    }
    
    public static implicit operator Password(string value) => new(value);
    public static implicit operator string(Password value) => value.Value;
    public override string ToString() => Value;
}