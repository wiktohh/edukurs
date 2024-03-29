namespace Domain.Exceptions;

public class InvalidRepositoryNameException : BaseException
{
    public InvalidRepositoryNameException() : base("Invalid repository name. Must not be empty.")
    {
    }
}