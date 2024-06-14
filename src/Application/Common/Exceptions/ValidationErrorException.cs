namespace ChinookStore.Application.Common.Exceptions;

public class ValidationErrorException(string message) : Exception(message);
