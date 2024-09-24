using MBS_QUERY.Contract.Services.Users;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MBS_QUERY.Infrastructure.Consumer.Abstractions.Messages;
using MediatR;

namespace MBS_QUERY.Infrastructure.Consumer.MessageBus.Consumers.Events;

public static class ProductConsumer
{
    public class ProductCreatedConsumer : Consumer<DomainEvent.ProductCreated>
    {
        public ProductCreatedConsumer(ISender sender, IMongoRepository<EventProjection> repository) : base(sender, repository)
        {
            
        }
    };

    public class ProductDeletedConsumer(ISender sender, IMongoRepository<EventProjection> repository) : Consumer<DomainEvent.ProductDeleted>(sender, repository);

    public class ProductUpdatedConsumer(ISender sender, IMongoRepository<EventProjection> repository) : Consumer<DomainEvent.ProductUpdated>(sender, repository);
}