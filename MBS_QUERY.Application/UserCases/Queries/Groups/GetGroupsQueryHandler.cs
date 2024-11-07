using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Groups;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MBS_QUERY.Application.UserCases.Queries.Groups;
public sealed class GetGroupsQueryHandler(
    IRepositoryBase<Group, Guid> groupRepository,
    ICurrentUserService currentUserService)
    : IQueryHandler<Query.GetGroupsQuery, List<Reponse.GroupResponse>>
{
    public async Task<Result<List<Reponse.GroupResponse>>> Handle(Query.GetGroupsQuery request,
        CancellationToken cancellationToken)
    {
        //lay Group Id cua may thang user 
        var groups =
            groupRepository.FindAll(x => x.Members.Any(x => x.StudentId.ToString() == currentUserService.UserId));

        var x = await groups.Select(x => new Reponse.GroupResponse
        {
            GroupId = x.Id,
            name = x.Name,
            mentorName = x.Mentor.FullName ?? "Has No Mentor Yet",
            leaderName = x.Leader.FullName,
            projectName = x.Project.Name ?? "Has No Project Yet"
        }).ToListAsync(cancellationToken);
        return Result.Success(x);
    }
}