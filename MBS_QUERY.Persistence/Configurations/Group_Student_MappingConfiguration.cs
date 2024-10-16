using MBS_QUERY.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MBS_QUERY.Persistence.Configurations;

public class Group_Student_MappingConfiguration : IEntityTypeConfiguration<Group_Student_Mapping>
{
    public void Configure(EntityTypeBuilder<Group_Student_Mapping> builder)
    {
        builder.HasKey(x => new { x.GroupId, x.StudentId });
    }
}