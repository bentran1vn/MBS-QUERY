namespace MBS_QUERY.Contract.Services.Skills;
public static class Response
{
    public record GetSkillsQuery(Guid Id, string Name);
}