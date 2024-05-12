using System.Runtime.CompilerServices;
using FluentValidation.Results;

namespace ChinookStore.Application._Common.Exceptions;

public class ArgumentErrorsException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public ArgumentErrorsException(IEnumerable<ValidationFailure> failures)
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ArgumentErrorsException(ValidationFailure failure)
        : this(new[] { failure })
    {
    }

    public ArgumentErrorsException(string propertyName, string errorMessage)
        : this(new ValidationFailure(propertyName, errorMessage))
    {
    }
}
