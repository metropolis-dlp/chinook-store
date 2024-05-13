namespace ChinookStore.Application._Common.Exceptions;

public class ValidationErrorsException : Exception
{
    public string[] Errors { get; protected set; }
    public ValidationErrorsException(string message)
    {
        Errors = [message];
    }

    public ValidationErrorsException(IEnumerable<string> messages)
    {
        Errors = messages.ToArray();
    }
}
