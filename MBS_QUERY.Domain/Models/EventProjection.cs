using MBS_QUERY.Contract.Attributes;
using MBS_QUERY.Domain.Abstractions.Entities;
using MBS_QUERY.Domain.Constrants;

namespace MBS_QUERY.Domain.Models;

[BsonCollection(TableNames.Event)]
public class EventProjection : Document
{
    public Guid EventId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}