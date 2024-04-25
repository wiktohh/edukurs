using Domain.Exceptions;

namespace Application.Exceptions;

public class UnauthorazedException : BaseException
{
    public UnauthorazedException(string msg) : base(msg)
    {
    }
}