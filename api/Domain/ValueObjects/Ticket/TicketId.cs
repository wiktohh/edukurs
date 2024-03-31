using Domain.Exceptions;

namespace Domain.ValueObjects.Ticket;

public record TicketId
{
    public Guid Value { get; }
    
    public TicketId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidTicketIdException();
        }
        Value = value;
    }
    
    public static implicit operator TicketId(Guid value) => new(value);
    public static implicit operator Guid(TicketId value) => value.Value;
    public override string ToString() => Value.ToString();
}