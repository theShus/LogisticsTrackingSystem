namespace LogisticsTrackingSystem.Api.Exceptions;

public class RepositoryException : Exception
{
    public RepositoryException(string message, Exception? innerException = null) 
        : base(message, innerException)
    {
    }
} 