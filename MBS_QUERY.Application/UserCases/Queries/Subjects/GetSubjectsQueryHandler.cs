
using AutoMapper;
using MassTransit.Initializers;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Subjects;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MBS_QUERY.Application.UserCases.Queries.Subjects;

public class GetSubjectsQueryHandler : IQueryHandler<Query.GetSubjectsQuery, List<Response.GetSubjectsQuery>>
{
    private readonly IRepositoryBase<Subject,Guid> _subjectsRepository;
    private readonly IMapper _mapper;

    public GetSubjectsQueryHandler(IRepositoryBase<Subject, Guid> subjectsRepository, IMapper mapper)
    {
        _subjectsRepository = subjectsRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<Response.GetSubjectsQuery>>> Handle(Query.GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        var query = await _subjectsRepository.FindAll(x => !x.IsDeleted).ToListAsync(cancellationToken);
        var result =  query.Select(x => new Response.GetSubjectsQuery(x.Id, x.Name)).ToList();
        return Result.Success(result);
    }
}