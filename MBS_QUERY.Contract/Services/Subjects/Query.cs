using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;

namespace MBS_QUERY.Contract.Services.Subjects;

public static class Query
{
    public record GetSubjectsQuery() : IQuery<List<Response.GetSubjectsQuery>>;
}