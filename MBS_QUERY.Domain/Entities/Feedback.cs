using MBS_QUERY.Domain.Abstractions.Entities;

namespace MBS_QUERY.Domain.Entities;
public class Feedback : Entity<Guid>, IAuditableEntity
{
    public string? Content { get; set; }
    public int Rating { get; set; }
    public Guid? ScheduleId { get; set; }
    public virtual Schedule? Schedule { get; set; }
    public bool IsMentor { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
}