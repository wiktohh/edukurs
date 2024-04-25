using Domain.Exceptions;

namespace Domain.ValueObjects.RepTask;

public record Deadline
{
    public DateTime Value { get; }
    public Deadline(DateTime value)
    {
        if (value < DateTime.Now)
        {
            throw new InvalidDateException();
        }
        Value = value;
    }
    
    public static implicit operator DateTime(Deadline deadline) => deadline.Value;
    public static implicit operator Deadline(DateTime deadline) => new(deadline);
    
    public override string ToString() => Value.ToString("f");

}