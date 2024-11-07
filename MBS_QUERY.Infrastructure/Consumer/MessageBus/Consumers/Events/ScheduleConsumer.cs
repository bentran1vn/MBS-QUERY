using MBS_CONTRACT.SHARE.Services.Slots;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MBS_QUERY.Infrastructure.Consumer.Abstractions.Messages;
using MediatR;

namespace MBS_QUERY.Infrastructure.Consumer.MessageBus.Consumers.Events;
public static class ScheduleConsumer
{
    public class ChangeStatusIntoBookedConsumer(ISender sender, IMongoRepository<EventProjection> _mongoRepository)
        : Consumer<DomainEvent.ChangeSlotStatusInToBooked>(sender, _mongoRepository);
}