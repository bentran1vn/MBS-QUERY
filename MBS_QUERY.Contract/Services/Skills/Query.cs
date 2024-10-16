using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;

namespace MBS_QUERY.Contract.Services.Skills;

public class Query
{
    public record GetSkillsQuery(int PageIndex, int PageSize) : IQuery<PagedResult<Response.GetSkillsQuery>>;
}