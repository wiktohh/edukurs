using Domain.Exceptions;

namespace Domain.ValueObjects.RepTask;

public record Title
{
    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length>100 || value.Length<5)
        {
            throw new InvalidTitleException();
        }
        Value = value;
    }

    public string Value { get; }
    
    public static implicit operator string(Title title) => title.Value;
    public static implicit operator Title(string title) => new(title);
    
    public override string ToString() => Value;
}