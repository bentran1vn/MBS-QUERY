using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Groups;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Query = MBS_QUERY.Contract.Services.Users.Query;

namespace MBS_QUERY.Application.UserCases.Queries.Users;

public class FindUserByEmailQueryHandler(IRepositoryBase<User, Guid> userRepository)
    : IQueryHandler<Query.FindUserByEmail, List<Reponse.Member>>
{
    public async Task<Result<List<Reponse.Member>>> Handle(Query.FindUserByEmail request,
        CancellationToken cancellationToken)
    {
        var users = userRepository.FindAll(x => x.Email.StartsWith(request.Email)).Take(request.Index);
        var response = await users.Select(x => new Reponse.Member
        {
            Email = x.Email,
            FullName = x.FullName
        }).ToListAsync(cancellationToken: cancellationToken);
        return Result.Success(response);
    }
}