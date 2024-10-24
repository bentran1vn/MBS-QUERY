using System.Linq.Expressions;
using AutoMapper;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Enumerations;
using MBS_QUERY.Contract.Services.Mentors;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MongoDB.Driver.Linq;

namespace MBS_QUERY.Application.UserCases.Queries.Mentors;

public class GetMentorsQueryHandler : IQueryHandler<Query.GetMentorsQuery, PagedResult<Response.GetAllMentorsResponse>>
{
    private readonly IMongoRepository<MentorProjection> _mentorRepository;
    private readonly IMapper _mapper;

    public GetMentorsQueryHandler(IMongoRepository<MentorProjection> mentorRepository, IMapper mapper)
    {
        _mentorRepository = mentorRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.GetAllMentorsResponse>>> Handle(Query.GetMentorsQuery request, CancellationToken cancellationToken)
    {
        var query = string.IsNullOrWhiteSpace(request.SearchTerm)
            ? _mentorRepository.AsQueryable(x => !x.IsDeleted)
            : _mentorRepository.AsQueryable(x =>
                (x.FullName.ToLower().Contains(request.SearchTerm.ToLower()) 
                 || x.MentorSkills.Any(skill => skill.Name.ToLower().Contains(request.SearchTerm.ToLower())))
                && !x.IsDeleted);
        
        query = request.SortOrder == SortOrder.Descending
            ? query.OrderByDescending(GetSortProperty(request))
            : query.OrderBy(GetSortProperty(request));
        
        var mentors = await PagedResult<MentorProjection>.CreateAsyncMongoLinq(query,
            request.PageIndex,
            request.PageSize);
        
        var result = _mapper.Map<PagedResult<Response.GetAllMentorsResponse>>(mentors);
        return Result.Success(result);
    }
    
    private static Expression<Func<MentorProjection, object>> GetSortProperty(Query.GetMentorsQuery request)
        => request.SortColumn?.ToLower() switch
        {
            "joinDate" => mentor => mentor.CreatedOnUtc,
            _ => mentor => mentor.CreatedOnUtc
            //_ => mentor => mentor.DocumentId // Default Sort Descending on CreatedDate column
        };
}