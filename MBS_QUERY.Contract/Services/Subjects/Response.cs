namespace MBS_QUERY.Contract.Services.Subjects;

public static class Response
{
    public record GetSubjectsQuery(Guid Id, string Name);
}