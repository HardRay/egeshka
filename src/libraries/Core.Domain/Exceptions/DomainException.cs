namespace Egeshka.Core.Domain.Exceptions;

public class DomainException(int code, string message) : ApplicationException(message)
{
    public int Code { get; } = code;

    public DomainException(string message) : this(code: -1, message)
    {
    }
}