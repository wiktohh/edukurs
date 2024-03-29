using Domain.Exceptions;

namespace Application.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string msg) : base(msg)
    {
    }
}