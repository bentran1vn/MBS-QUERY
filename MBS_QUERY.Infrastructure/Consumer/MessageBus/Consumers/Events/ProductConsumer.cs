using MBS_QUERY.Contract.Services.Users;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MBS_QUERY.Infrastructure.Consumer.Abstractions.Messages;
using MediatR;
using Serilog;

namespace MBS_QUERY.Infrastructure.Consumer.MessageBus.Consumers.Events;

public static class ProductConsumer
{
    // public class ProductCreatedConsumer(ISender sender, IMongoRepository<EventProjection> repository, ILogger log)
    //     : Consumer<DomainEvent.ProductCreated>(sender, repository, log);
    //
    // public class ProductDeletedConsumer(ISender sender, IMongoRepository<EventProjection> repository)
    //     : Consumer<DomainEvent.ProductDeleted>(sender, repository);
    //
    // public class ProductUpdatedConsumer(ISender sender, IMongoRepository<EventProjection> repository)
    //     : Consumer<DomainEvent.ProductUpdated>(sender, repository);

}