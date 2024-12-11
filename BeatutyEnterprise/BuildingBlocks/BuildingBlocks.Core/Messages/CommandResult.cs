using FluentValidation.Results;

namespace BuildingBlocks.Core.Messages;

public class CommandResult<T>
{
    public CommandResult(T result)
    {
        Result = result;
        ValidationResult = new ValidationResult();
    }

    public T? Result { get; set; }

    public ValidationResult ValidationResult { get; set; }

    public CommandResult()
    {
        ValidationResult = new ValidationResult();
    }

    public static CommandResult<T> Create(ValidationResult validationResult)
    {
        return new CommandResult<T>
        {
            ValidationResult = validationResult ?? new ValidationResult()
        };
    }

    public void AddError(string propertyName, string errorMessage)
    {
        var error = new ValidationFailure(propertyName, errorMessage);
        ValidationResult.Errors.Add(error);
    }

    public static CommandResult<T> Fail(string errorMessage)
    {
        var validationResult = new ValidationResult();

        validationResult.Errors.Add(new ValidationFailure(string.Empty, errorMessage));

        return new CommandResult<T>
        {
            ValidationResult = validationResult
        };
    }
}