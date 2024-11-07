using MBS_QUERY.Domain.Abstractions.Entities;

namespace MBS_QUERY.Domain.Entities;
public class Skill : Entity<Guid>, IAuditableEntity
{
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = default!;
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual IReadOnlyCollection<MentorSkills> MentorSkillsList { get; set; } = default!;
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
}