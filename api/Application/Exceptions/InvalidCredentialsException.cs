using Domain.Exceptions;

namespace Application.Exceptions;

public class InvalidCredentialsException : BaseException
{
    public InvalidCredentialsException() : base("email or password is invalid")
    {
    }
}