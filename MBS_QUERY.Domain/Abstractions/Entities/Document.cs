using MongoDB.Bson;

namespace MBS_QUERY.Domain.Abstractions.Entities;

public abstract class Document : IDocument
{
    public ObjectId  Id { get; set; }
    public Guid DocumentId { get; set; } // Id cua SourceMessage: ProductID, CustomerID, OrderID
    public DateTimeOffset CreatedOnUtc => Id.CreationTime;
    public DateTimeOffset? ModifiedOnUtc { get; set; }
}