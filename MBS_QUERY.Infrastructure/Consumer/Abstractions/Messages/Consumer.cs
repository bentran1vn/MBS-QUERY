using MassTransit;
using MBS_CONTRACT.SHARE.Abstractions.Messages;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MediatR;

namespace MBS_QUERY.Infrastructure.Consumer.Abstractions.Messages;
public abstract class Consumer<TMessage>(ISender sender, IMongoRepository<EventProjection> repository)
    : IConsumer<TMessage>
    where TMessage : class, IDomainEvent
{
    public async Task Consume(ConsumeContext<TMessage> context)
    {
        // Find By EventId
        // => Exist Ignore
        // => Not Exist Send Event
        var eventProjection = await repository.FindOneAsync(e => e.EventId.Equals(context.Message.IdEvent));

        if (eventProjection is null)
        {
            await sender.Send(context.Message);

            Console.WriteLine(context.Message.ToString());

            eventProjection = new EventProjection
            {
                EventId = context.Message.IdEvent,
                Name = context.Message.GetType().Name,
                Type = context.Message.GetType().Name
            };
            await repository.InsertOneAsync(eventProjection);
        }
    }
}