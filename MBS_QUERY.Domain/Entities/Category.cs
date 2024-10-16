using MBS_QUERY.Domain.Abstractions.Entities;

namespace MBS_QUERY.Domain.Entities;

public class Category : Entity<Guid>, IAuditableEntity
{
    public string Name { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
    public virtual IReadOnlyCollection<Skill> SkillList { get; set; } = default!;
}
