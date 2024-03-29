using Domain.Exceptions;

namespace Domain.ValueObjects.Repository;

public record Name
{
    public string Value { get; private set; }
    
    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidRepositoryNameException();
        }
        Value = value;
    }
    
    public static implicit operator Name(string value) => new(value);
    public static implicit operator string(Name name) => name.Value;
    public override string ToString() => Value;
}