namespace Ordering.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base($"Domain Exception thrown: \"{message}\".")
    {
    }

    public DomainException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}
