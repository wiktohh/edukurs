namespace Domain.Exceptions;

public class InvalidTitleException : BaseException
{
    public InvalidTitleException( ) : base("Invalid title")
    {
    }
}