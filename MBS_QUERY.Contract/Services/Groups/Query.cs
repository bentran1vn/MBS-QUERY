using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;

namespace MBS_QUERY.Contract.Services.Groups;

public static class Query
{
    public record GetGroupsQuery() : IQuery<List<Reponse.GroupResponse>>;
    
    public record GetGroupDetail(Guid GroupId) : IQuery<Reponse.GroupDetailResponse>;
}