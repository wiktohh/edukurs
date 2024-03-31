namespace Domain.Exceptions;

public class InvalidStatusException : BaseException
{
    public InvalidStatusException( ) : base("Invalid status. Status must be 'open', 'closed', 'in progress', 'resolved' or 'rejected'.")
    {
    }
}