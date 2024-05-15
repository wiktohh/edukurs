using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.ValueObjects.User;

public record FirstName
{
    public string Value { get; init; }
    Regex regex = new Regex(@"^[a-zA-Z]+$");
    public FirstName(string value)
    {
        value = value.Trim();
        if (string.IsNullOrWhiteSpace(value) || value.Length < 2 || value.Length > 50 || !regex.IsMatch(value))
        {
            throw new InvalidFirstNameException(value);
        }
        Value = value;
    }
    
    public static implicit operator FirstName(string value) => new(value);
    public static implicit operator string(FirstName value) => value.Value;
    public override string ToString() => Value;
}
