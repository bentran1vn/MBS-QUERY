
using AutoMapper;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Subjects;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;

namespace MBS_QUERY.Application.UserCases.Queries.Subjects;

public class GetSubjectsQueryHandler : IQueryHandler<Query.GetSubjectsQuery, PagedResult<Response.GetSubjectsQuery>>
{
    private readonly IRepositoryBase<Subject,Guid> _subjectsRepository;
    private readonly IMapper _mapper;

    public GetSubjectsQueryHandler(IRepositoryBase<Subject, Guid> subjectsRepository, IMapper mapper)
    {
        _subjectsRepository = subjectsRepository;
        _mapper = mapper;
    }
    public async Task<Result<PagedResult<Response.GetSubjectsQuery>>> Handle(Query.GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        var query = _subjectsRepository.FindAll(x => !x.IsDeleted);
        var subjects = await PagedResult<Subject>.CreateAsync(query, request.PageIndex, request.PageSize);
        return Result.Success(_mapper.Map<PagedResult<Response.GetSubjectsQuery>>(subjects));
    }
}