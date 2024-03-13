namespace Domain.Exceptions;

public class InvalidRoleException : BaseException
{
    public InvalidRoleException(string role) : base($"The role {role} is invalid.")
    {
    }
}