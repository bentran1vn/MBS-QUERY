using MBS_QUERY.Contract.Attributes;
using MBS_QUERY.Domain.Abstractions.Entities;
using MBS_QUERY.Domain.Constrants;

namespace MBS_QUERY.Domain.Documents;
[BsonCollection(TableNames.Slot)]
public class SlotProjection : Document
{
    public Guid SlotId { get; set; }
    public Guid? MentorId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
    public bool IsOnline { get; set; }
    public string? Note { get; set; }
    public short? Month { get; set; }
    public bool IsBook { get; set; }
    public bool IsDeleted { get; set; }
}