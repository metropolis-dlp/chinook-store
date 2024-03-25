namespace ChinookStore.Application._Common.Exceptions;

public class ValidationErrorsException : Exception
{
    public Dictionary<string, string[]> Errors = new ();
}