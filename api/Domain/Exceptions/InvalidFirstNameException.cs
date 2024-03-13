namespace Domain.Exceptions;

public class InvalidFirstNameException : BaseException
{
    public InvalidFirstNameException(string username) : base($"The first name {username} is invalid.")
    {
        
    }
}