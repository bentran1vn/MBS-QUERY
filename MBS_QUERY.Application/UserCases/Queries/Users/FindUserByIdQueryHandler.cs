using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Groups;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;
using Query = MBS_QUERY.Contract.Services.Users.Query;

namespace MBS_QUERY.Application.UserCases.Queries.Users;

public class FindUserByIdQueryHandler : IQueryHandler<Query.FindUserById,Reponse.MemberDetail>
{
    private readonly IRepositoryBase<User,Guid> _userRepository;

    public FindUserByIdQueryHandler(IRepositoryBase<User, Guid> userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<Reponse.MemberDetail>> Handle(Query.FindUserById request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.Id,cancellationToken);
        if (user == null)
        {
            return (Result<Reponse.MemberDetail>)Result.Failure(new Error("404","User not found"));
        }
        return Result.Success(new Reponse.MemberDetail
        {
            Email = user.Email,
            FullName = user.FullName,
            Points = user.Points
        });
    }
}