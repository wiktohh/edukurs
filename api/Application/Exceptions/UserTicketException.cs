using Domain.Exceptions;

namespace Application.Exceptions;

public class UserTicketException : BaseException
{
    public UserTicketException(string message) : base(message)
    {
    }
}