using MBS_CONTRACT.SHARE.Services.Slots;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MediatR;
using MBS_QUERY.Infrastructure.Consumer.Abstractions.Messages;

namespace MBS_QUERY.Infrastructure.Consumer.MessageBus.Consumers.Events;
public static class SlotConsumer
{
    public class SlotUpdatedConsumer(ISender sender, IMongoRepository<EventProjection> _mongoRepository)
        : Consumer<DomainEvent.SlotUpdated>(sender, _mongoRepository);
}