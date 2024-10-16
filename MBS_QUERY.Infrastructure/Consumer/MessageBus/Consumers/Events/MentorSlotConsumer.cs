using MBS_CONTRACT.SHARE.Services.Users;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MBS_QUERY.Infrastructure.Consumer.Abstractions.Messages;
using MediatR;

namespace MBS_QUERY.Infrastructure.Consumer.MessageBus.Consumers.Events;

public static class MentorSlotConsumer
{
    public class MentorSlotCreatedConsumer(ISender sender, IMongoRepository<EventProjection> repository)
        : Consumer<DomainEvent.MentorSlotCreated>(sender, repository);
}
