namespace Domain.Exceptions;

public class InvalidLastNameException : BaseException
{
    public InvalidLastNameException(string lastname) : base($"The last name {lastname} is invalid.")
    {
        
    }
}