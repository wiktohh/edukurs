using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.ValueObjects.User;

public record LastName
{
    public string Value { get; }
    Regex regex = new Regex(@"^[a-zA-Z]+$");
    public LastName(string value)
    {
        value = value.Trim();
        if ( string.IsNullOrWhiteSpace(value) || value.Length < 2 || value.Length > 50 || !regex.IsMatch(value))
        {
            throw new InvalidLastNameException(value);
        }
        Value = value;
    }
    
    public static implicit operator LastName(string value) => new(value);
    public static implicit operator string(LastName value) => value.Value;
    public override string ToString() => Value;
}