using MBS_QUERY.Domain.Abstractions.Entities;

namespace MBS_QUERY.Domain.Entities;

public class Project : Entity<Guid>, IAuditableEntity
{
    public string Name { get ; set ; }
    public string Description { get ; set ; }
    public DateTimeOffset CreatedOnUtc { get ; set ; }
    public DateTimeOffset? ModifiedOnUtc { get ; set ; }
}
