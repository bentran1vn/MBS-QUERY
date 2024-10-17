﻿using MBS_QUERY.Domain.Abstractions.Entities;

namespace MBS_QUERY.Domain.Entities;

public class Schedule : Entity<Guid>, IAuditableEntity
{
    public Guid MentorId { get; set; }
    public virtual User? Mentor { get; set; }

    public Guid GroupId { get; set; }
    public virtual Group? Group { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
    public Guid SubjectId { get; set; }
    public bool IsBooked { get; set; }=false;
    public virtual Subject? Subject { get; set; }
    public DateTimeOffset CreatedOnUtc { get ; set ; }
    public DateTimeOffset? ModifiedOnUtc { get ; set ; }
}