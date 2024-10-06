using MBS_COMMAND.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Services.Mentors;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MongoDB.Driver;


namespace MBS_QUERY.Application.UserCases.Queries.Mentors;

public class GetAllMentorsQueryHandler : IQueryHandler<Query.GetAllMentorsQuery, List<Response.GetAllMentorsResponse>>
{
    private readonly IMongoRepository<MentorProjection> _mentorRepository;

    public GetAllMentorsQueryHandler(IMongoRepository<MentorProjection> mentorRepository)
    {
        _mentorRepository = mentorRepository;
    }

    public async Task<Result<List<Response.GetAllMentorsResponse>>> Handle(Query.GetAllMentorsQuery request, CancellationToken cancellationToken)
    {
        var mentors = await _mentorRepository.AsQueryable().ToListAsync(cancellationToken);
        var result = new List<Response.GetAllMentorsResponse>();

        foreach (var item in mentors)
        {
            result.Add(new Response.GetAllMentorsResponse()
            {
                Name = item.FullName,
                Id = item.DocumentId
            });
        }
        return Result.Success(result);
    }
}