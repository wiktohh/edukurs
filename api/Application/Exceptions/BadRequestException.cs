using Domain.Exceptions;

namespace Application.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(string message) : base(message)
    {
    }
}