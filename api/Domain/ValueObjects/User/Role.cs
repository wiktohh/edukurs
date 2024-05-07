using Domain.Exceptions;

namespace Domain.ValueObjects.User;

public record Role
{
    public static IEnumerable<string> AvailableRoles => new List<string>
    {
        "Teacher",
        "Student",
        "Admin"
    };

    public string Value { get; }

    public Role(string value)
    {
        if (string.IsNullOrWhiteSpace(value) ||  !AvailableRoles.Contains(value) || value.Length > 30)
        {
            throw new InvalidRoleException(value);
        }
        Value = value;
    }
    
    public static Role Teacher => new("Teacher");
    public static Role Student => new("Student");
    
    public static implicit operator Role(string value) => new Role(value);
    public static implicit operator string(Role value) => value?.Value;
    public override string ToString() => Value;
}