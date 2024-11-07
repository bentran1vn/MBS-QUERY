using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MBS_QUERY.Domain.Abstractions.Entities;
public interface IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    ObjectId Id { get; set; }

    DateTime CreatedOnUtc { get; set; }
    DateTime? ModifiedOnUtc { get; }
}