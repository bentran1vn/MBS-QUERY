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
                Id = item.DocumentId,
                FullName = item.FullName,
                Email = item.Email,
                Point = item.Points,
                CreatedOnUtc = item.CreatedOnUtc,
                Skills = item.MentorSkills.Select(skill => new Response.Skill()
                {
                    SkillName = skill.Name,
                    SkillDesciption = skill.Description,
                    SkillCategoryType = skill.CateogoryType,
                    CreatedOnUtc = skill.CreatedOnUtc,
                    Cetificates = skill.SkillCetificates.Select(cer => new Response.Cetificate()
                    {
                        CetificateName = cer.Name,
                        CetificateDesciption = cer.Description,
                        CreatedOnUtc = cer.CreatedOnUtc,
                        CetificateImageUrl = cer.ImageUrl
                    }).ToList()
                }).ToList()
            });
        }
        return Result.Success(result);
    }
}