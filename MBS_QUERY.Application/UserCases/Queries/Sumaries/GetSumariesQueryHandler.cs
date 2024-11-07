using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Sumaries;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MBS_QUERY.Application.UserCases.Queries.Sumaries;
public class GetSumariesQueryHandler : IQueryHandler<Query.GetSumariesQuery, Response.GetSumariesQuery>
{
    private readonly IRepositoryBase<Group, Guid> _groupRepository;
    private readonly IRepositoryBase<User, Guid> _userRepository;

    public GetSumariesQueryHandler(IRepositoryBase<User, Guid> userRepository,
        IRepositoryBase<Group, Guid> groupRepository)
    {
        _userRepository = userRepository;
        _groupRepository = groupRepository;
    }

    public async Task<Result<Response.GetSumariesQuery>> Handle(Query.GetSumariesQuery request,
        CancellationToken cancellationToken)
    {
        // Materialize queries to avoid concurrent DbContext access
        var totalMentorActive =
            await _userRepository.FindAll(x => x.Role == 1 && x.Status == 1).CountAsync(cancellationToken);
        var totalStudentActive =
            await _userRepository.FindAll(x => x.Role == 2 && x.Status == 1).CountAsync(cancellationToken);
        var totalGroupActive = await _groupRepository.FindAll().CountAsync(cancellationToken);
        return Result.Success(new Response.GetSumariesQuery(totalMentorActive, totalStudentActive, totalGroupActive));


    }
}