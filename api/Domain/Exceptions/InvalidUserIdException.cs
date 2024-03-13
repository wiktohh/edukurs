namespace Domain.Exceptions;

public class InvalidUserIdException : BaseException
{
    public InvalidUserIdException(string Guid) : base($"The user id {Guid} is invalid.")
    {
    }
}