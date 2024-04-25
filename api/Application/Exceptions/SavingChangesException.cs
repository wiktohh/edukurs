using Domain.Exceptions;

namespace Application.Exceptions;

public class SavingChangesException : BaseException
{
    public SavingChangesException(string entity) : base($"Error saving changes to {entity}")
    {
    }
}