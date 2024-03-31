using Domain.Exceptions;

namespace Domain.ValueObjects.Ticket;

public class Status
{
    public static IEnumerable<string> AvailableStatuses => new List<string>
    {
        "Pending",
        "Approved",
        "Rejected"
    };

    public string Value { get; }

    public Status(string value)
    {
        if (string.IsNullOrWhiteSpace(value) ||  !AvailableStatuses.Contains(value) || value.Length > 30)
        {
            throw new InvalidStatusException();
        }
        Value = value;
    }    
    public static Status Pending => new("Pending");
    public static Status Approved => new("Approved");
    public static Status Rejected => new("Rejected");
    public static implicit operator Status(string value) => new Status(value);
    public static implicit operator string(Status value) => value.Value;
    public override string ToString() => Value;
}