using MBS_COMMAND.Contract.Abstractions.Shared;

namespace MBS_QUERY.Contract.Abstractions.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError = new("Validation Error", "A validation error occured");
    Error[] Errors { get; }
}
