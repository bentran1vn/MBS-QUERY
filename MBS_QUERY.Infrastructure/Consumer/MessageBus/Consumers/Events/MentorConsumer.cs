using MBS_QUERY.Contract.Services.Mentors;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MBS_QUERY.Infrastructure.Consumer.Abstractions.Messages;
using MediatR;
using Serilog.Core;

namespace MBS_QUERY.Infrastructure.Consumer.MessageBus.Consumers.Events;

public static class MentorConsumer
{
    public class MentorCreatedConsumer(ISender sender, IMongoRepository<EventProjection> repository)
        : Consumer<DomainEvent.MentorCreated>(sender, repository);
}