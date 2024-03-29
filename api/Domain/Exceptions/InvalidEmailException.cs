namespace Domain.Exceptions;

public class InvalidEmailException : BaseException
{
    public InvalidEmailException(string value) : base($"Invalid email: {value}")
    {
    }
}