﻿namespace MBS_QUERY.Domain.Entities;

public class Group_Student_Mapping
{
    public Guid GroupId { get; set; }
    public virtual Group Group { get; set; }
    public Guid StudentId { get; set; }
    public virtual User Student { get; set; }
}
