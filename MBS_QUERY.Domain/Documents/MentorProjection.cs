using MBS_QUERY.Contract.Attributes;
using MBS_QUERY.Domain.Abstractions.Entities;
using MBS_QUERY.Domain.Constrants;

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
}