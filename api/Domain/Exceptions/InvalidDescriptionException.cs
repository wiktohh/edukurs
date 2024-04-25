namespace Domain.Exceptions;

public class InvalidDescriptionException : BaseException
{
    public InvalidDescriptionException() : base("Invalid description")
    {
    }
}