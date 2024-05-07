namespace Domain.Exceptions;

public class InvalidReportIdException : BaseException
{
    public InvalidReportIdException(string message) : base(message)
    {
    }
}