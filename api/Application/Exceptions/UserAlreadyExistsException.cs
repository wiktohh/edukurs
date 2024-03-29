using Domain.Exceptions;

namespace Application.Exceptions;

public class UserAlreadyExistsException : BaseException
{
    public UserAlreadyExistsException(string message) : base($"user with email {message} already exists")
    {
    }
}