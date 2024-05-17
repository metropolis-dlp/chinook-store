using System.Runtime.CompilerServices;
using FluentValidation.Results;

namespace ChinookStore.Application._Common.Exceptions;

public class InvalidArgumentsErrorsException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public InvalidArgumentsErrorsException(IEnumerable<ValidationFailure> failures)
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public InvalidArgumentsErrorsException(ValidationFailure failure)
        : this(new[] { failure })
    {
    }

    public InvalidArgumentsErrorsException(string propertyName, string errorMessage)
        : this(new ValidationFailure(propertyName, errorMessage))
    {
    }
}
