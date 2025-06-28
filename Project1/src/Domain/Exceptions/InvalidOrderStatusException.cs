namespace Project1.Domain.Exceptions;

public class InvalidOrderStatusException : Exception
{
    public InvalidOrderStatusException(string message) : base(message)
    {
    }
}
