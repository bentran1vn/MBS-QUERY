using AutoMapper;
using MBS_COMMAND.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Mentors;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MongoDB.Driver;


namespace MBS_QUERY.Application.UserCases.Queries.Mentors;

public class GetAllMentorsQueryHandler : IQueryHandler<Query.GetAllMentorsQuery, PagedResult<Response.GetAllMentorsResponse>>
{
    private readonly IMongoRepository<MentorProjection> _mentorRepository;
    private readonly IMapper _mapper;

    public GetAllMentorsQueryHandler(IMongoRepository<MentorProjection> mentorRepository, IMapper mapper)
    {
        _mentorRepository = mentorRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.GetAllMentorsResponse>>> Handle(Query.GetAllMentorsQuery request, CancellationToken cancellationToken)
    {
        var query = _mentorRepository.AsQueryable();
        
        var mentors = await PagedResult<MentorProjection>.CreateAsyncMongoLinq(query,
            request.pageIndex,
            request.pageSize);
        
        var result = _mapper.Map<PagedResult<Response.GetAllMentorsResponse>>(mentors);
        return Result.Success(result);
        
    }
}