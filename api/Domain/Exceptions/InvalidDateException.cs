namespace Domain.Exceptions;

public class InvalidDateException : BaseException
{
    public InvalidDateException() : base("Date cannot be in the past.")
    {
    }
}