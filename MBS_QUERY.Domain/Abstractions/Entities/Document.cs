using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MBS_QUERY.Domain.Abstractions.Entities;

public abstract class Document : IDocument
{
    public ObjectId  Id { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    
    public DateTime? ModifiedOnUtc { get; set; }
    public Guid DocumentId { get; set; } // Id cua SourceMessage: ProductID, CustomerID, OrderI
}