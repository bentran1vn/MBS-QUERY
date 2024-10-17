using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Groups;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;

namespace MBS_QUERY.Application.UserCases.Queries.Groups;

public class GetGroupDetailQueryHandler(IRepositoryBase<Group, Guid> groupRepository)
    : IQueryHandler<Query.GetGroupDetail, Reponse.GroupDetailResponse>
{
    public async Task<Result<Reponse.GroupDetailResponse>> Handle(Query.GetGroupDetail request,
        CancellationToken cancellationToken)
    {
        var group = await groupRepository.FindByIdAsync(request.GroupId, cancellationToken);
        var name = group.Mentor?.FullName ?? "Has No Mentor Yet";
        var leaderFullName = group.Leader?.FullName ?? "Has No Leader Yet";
        var ProjectName = group.Project?.Name ?? "Has No Project Yet";
        var Members = group.Members.Select(x => new Reponse.Member
        {
            Email = x.Student.Email,
            FullName = x.Student.FullName
        }).ToList();
        var response = new Reponse.GroupDetailResponse
        {
            name = group.Name,
            mentorName =name,
            leaderName = leaderFullName,
            projectName = ProjectName,
            Stack = group.Stack,
            Members = Members,
        };
        return Result.Success(response);
    }
}