using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MBS_QUERY.Domain.Abstractions.Entities;

public interface IDocument
{
    [BsonId ]
    [BsonRepresentation(BsonType.String)]
    ObjectId Id { get; set; }
    DateTimeOffset CreatedOnUtc { get; }
    DateTimeOffset? ModifiedOnUtc { get; }
}  