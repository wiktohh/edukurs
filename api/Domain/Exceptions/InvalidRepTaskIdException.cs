namespace Domain.Exceptions;

public class InvalidRepTaskIdException : BaseException
{
    public InvalidRepTaskIdException() : base("Invalid RepTask Id")
    {
    }
}