namespace Domain.Exceptions;

public class InvalidPasswordException : BaseException
{
    public InvalidPasswordException() : base("The password is invalid.")
    {
    }
}