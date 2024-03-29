namespace Domain.Exceptions;

public class InvalidRepositoryIdException : BaseException
{
    public InvalidRepositoryIdException() : base("Invalid repository id.")
    {
    }
}