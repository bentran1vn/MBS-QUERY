using MBS_COMMAND.Contract.Abstractions.Shared;
using MediatR;

namespace MBS_QUERY.Contract.Abstractions.Messages;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
