using MBS_QUERY.Domain.Abstractions.Entities;


namespace MBS_QUERY.Domain.Entities;

public class MentorSkills : Entity<Guid>, IAuditableEntity
{
    public Guid SkillId { get; set; }
    public virtual Skill Skill { get; set; } = default!;
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
    public virtual IReadOnlyCollection<Certificate> CertificateList { get; set; } = default!;
}