namespace Domain.Exceptions;

public class InvalidTicketIdException : BaseException
{
    public InvalidTicketIdException() : base("Invalid ticket id.")
    {
    }
}