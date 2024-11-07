using MBS_QUERY.Contract.Abstractions.Messages;

namespace MBS_QUERY.Contract.Services.Sumaries;
public static class Query
{
    public record GetSumariesQuery : IQuery<Response.GetSumariesQuery>;
}

public static class Response
{
    public record GetSumariesQuery(
        int TotalMentorActive,
        int TotalStudentActive,
        int TotalGroupActive
    );
}