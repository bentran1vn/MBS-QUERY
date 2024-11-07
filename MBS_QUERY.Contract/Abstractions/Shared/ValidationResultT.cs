namespace MBS_QUERY.Contract.Abstractions.Shared;
public class ValidationResult<Tvalue> : Result<Tvalue>, IValidationResult
{
    protected internal ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationResult<Tvalue> WithErrors(Error[] errors)
    {
        return new ValidationResult<Tvalue>(errors);
    }
}