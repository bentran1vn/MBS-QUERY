using MBS_QUERY.Domain.Abstractions.Entities;

namespace MBS_QUERY.Domain.Entities;

public class Subject : Entity<Guid>, IAuditableEntity
{
    public string Name { get ; set ; }
    public int Status { get ; set ; }
    public Guid SemesterId { get; set; }
    public virtual Semester? Semester { get; set; }


    public DateTimeOffset CreatedOnUtc { get ; set ; }
    public DateTimeOffset? ModifiedOnUtc { get ; set ; }
}
