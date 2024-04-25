using Domain.Exceptions;

namespace Domain.ValueObjects.RepTask;

public record RepTaskId
{
    public Guid Value { get; }
    
    public RepTaskId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidRepTaskIdException();
        }
        Value = value;
    }
    
    public static implicit operator RepTaskId(Guid value) => new(value);
    public static implicit operator Guid(RepTaskId value) => value.Value;
    public override string ToString() => Value.ToString();
}