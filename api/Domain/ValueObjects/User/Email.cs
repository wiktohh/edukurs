using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.ValueObjects.User;

public record Email
{
    Regex regex = new(@"^([\w\.\-]+)@polsl\.pl");
    
    
    
    public string Value { get; init; }

    public Email(string value)
    {
        if(!regex.IsMatch(value))
        {
            throw new InvalidEmailException(value);
        }
        
        if ( string.IsNullOrWhiteSpace(value) || value.Length < 2 || value.Length > 50)
        {
            throw new InvalidEmailException(value);
        }
        Value = value;
    }
    
    public static implicit operator Email(string value) => new(value);
    public static implicit operator string(Email value) => value.Value;
    public override string ToString() => Value;
}