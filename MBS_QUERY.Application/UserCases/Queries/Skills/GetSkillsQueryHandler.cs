using AutoMapper;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;
using Query = MBS_QUERY.Contract.Services.Skills.Query;
using Response = MBS_QUERY.Contract.Services.Skills.Response;

namespace MBS_QUERY.Application.UserCases.Queries.Skills;
public class GetSkillsQueryHandler : IQueryHandler<Query.GetSkillsQuery, PagedResult<Response.GetSkillsQuery>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryBase<Skill, Guid> _skillRepository;

    public GetSkillsQueryHandler(IRepositoryBase<Skill, Guid> skillRepository, IMapper mapper)
    {
        _skillRepository = skillRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.GetSkillsQuery>>> Handle(Query.GetSkillsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _skillRepository.FindAll(x => !x.IsDeleted);

        var skills = await PagedResult<Skill>.CreateAsync(query,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.GetSkillsQuery>>(skills);

        return Result.Success(result);
    }
}