using MBS_COMMAND.Contract.Abstractions.Shared;
using MediatR;

namespace MBS_QUERY.Contract.Abstractions.Messages;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
