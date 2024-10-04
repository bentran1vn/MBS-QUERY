using MassTransit;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MediatR;
using Serilog;

namespace MBS_QUERY.Infrastructure.Consumer.Abstractions.Messages;

public abstract class Consumer<TMessage> : IConsumer<TMessage>
    where TMessage : class, IDomainEvent
{
    private readonly ISender _sender;
    private readonly IMongoRepository<EventProjection> _repository;
     protected Consumer(ISender sender, IMongoRepository<EventProjection> repository)
     {
         _sender = sender;
         _repository = repository;
     }
    
    public async Task Consume(ConsumeContext<TMessage> context)
    {
        // Find By EventId
        // => Exist Ignore
        // => Not Exist Send Event
        var eventProjection = await _repository.FindOneAsync(e => e.EventId.Equals(context.Message.IdEvent));

        if (eventProjection is null)
        {
            await _sender.Send(context.Message);

            Console.WriteLine("Kakaka");
            Console.WriteLine(context.Message.ToString());
            
            eventProjection = new EventProjection()
            {
                EventId = context.Message.IdEvent,
                Name = context.Message.GetType().Name,
                Type = context.Message.GetType().Name
            };
            await _repository.InsertOneAsync(eventProjection);
        }
    }
}

