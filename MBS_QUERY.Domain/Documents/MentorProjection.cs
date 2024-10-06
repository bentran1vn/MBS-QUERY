using MBS_QUERY.Contract.Attributes;
using MBS_QUERY.Domain.Abstractions.Entities;
using MBS_QUERY.Domain.Constrants;
using MongoDB.Bson.Serialization.Attributes;

namespace MBS_QUERY.Domain.Documents;

[BsonCollection(TableNames.Mentor)]
public class MentorProjection : Document
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public int Role { get; set; }
    public int Points { get; set; }
    public int Status { get; set; }
    public bool IsDeleted { get; set; }
    public List<SkillProjection> MentorSkills { get; set; }
}

public class SkillProjection : Document
{
    // public Guid DocumentId { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public string CateogoryType { get; set; }
    public List<CertificateProjection> SkillCetificates { get; set; }
}

public class CertificateProjection : Document
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    
}