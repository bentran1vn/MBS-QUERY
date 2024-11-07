using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Mentors;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MBS_QUERY.Application.UserCases.Queries.Mentors;
public class
    ShowListMentorQueryHandler(IRepositoryBase<User, Guid> userRepository)
    : IQueryHandler<Query.ShowListMentorQuery, List<Response.ShowListMentorResponse>>
{

    public async Task<Result<List<Response.ShowListMentorResponse>>> Handle(Query.ShowListMentorQuery request,
        CancellationToken cancellationToken)
    {
        var mentors = await userRepository.FindAll(x => x.Role == 1 && x.Status == 0).Select(x =>
            new Response.ShowListMentorResponse
            {
                Id = x.Id,
                FullName = x.FullName,
                CreatedAtUtc = x.CreatedOnUtc
            }).ToListAsync(cancellationToken);

        return Result.Success(mentors);
    }
}