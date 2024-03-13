using Domain.Exceptions;

namespace Domain.ValueObjects.User;

public record UserId
{
    public Guid Value { get; }
    
    public UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidUserIdException(value.ToString());
        }
        Value = value;
    }
    
    public static implicit operator UserId(Guid value) => new(value);
    public static implicit operator Guid(UserId value) => value.Value;
    public override string ToString() => Value.ToString();
}