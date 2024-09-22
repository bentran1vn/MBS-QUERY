using MassTransit;

namespace MBS_QUERY.Contract.Abstractions.Messages;

[ExcludeFromTopology]
public interface IDomainEvent
{
    public Guid IdEvent { get; init; }
}