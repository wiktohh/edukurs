using Domain.Exceptions;

namespace Domain.ValueObjects.RepTask;

public record Description
{
    public Description(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 1000)
        {
            throw new InvalidDescriptionException();
        }
        Value = value;
    }

    public string Value { get; }
    
    public static implicit operator string(Description description) => description.Value;
    public static implicit operator Description(string description) => new(description);
    
    public override string ToString() => Value;
}