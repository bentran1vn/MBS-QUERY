using MBS_QUERY.Application.Abstractions;
using MBS_QUERY.Contract.Services.Mentors;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;

namespace MBS_QUERY.Application.UserCases.Events.MentorSkills;
using DomainEventShared = MBS_CONTRACT.SHARE.Services.MentorSkills.DomainEvent;
using AbsrationShared = MBS_CONTRACT.SHARE.Abstractions.Shared;


public class MentorSkillsCreatedHandler : MBS_CONTRACT.SHARE.Abstractions.Messages.ICommandHandler<DomainEventShared.MentorSkillsCreated>
{
    private readonly IMongoRepository<MentorProjection> _mentorRepository;
    private readonly ICacheService _cacheService;

    public MentorSkillsCreatedHandler(IMongoRepository<MentorProjection> mentorRepository, ICacheService cacheService)
    {
        _mentorRepository = mentorRepository;
        _cacheService = cacheService;
    }

    public async Task<AbsrationShared.Result> Handle(DomainEventShared.MentorSkillsCreated request, CancellationToken cancellationToken)
    {
        var existMentor =  await _mentorRepository.FindOneAsync(p => p.DocumentId.Equals(request.MentorId));

        if (existMentor is null)
        {
            return AbsrationShared.Result.Success();
        }
        else
        {
            var skills = new SkillProjection()
            {
                DocumentId = request.Id,
                SkillCetificates = request.Certificates.Select(x => new CertificateProjection()
                {
                    DocumentId = Guid.NewGuid(),
                    Description = x.Description,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    CreatedOnUtc = request.skill.CreatedOnUtc.DateTime
                }).ToList(),
                Name = request.skill.Name,
                Description = request.skill.Description,
                CateogoryType = request.skill.CateogoryType,
                CreatedOnUtc = request.skill.CreatedOnUtc.DateTime
            };

            List<SkillProjection> skillList;
            
            if (!existMentor.MentorSkills.Any())
            {
                skillList = new List<SkillProjection>();
            }
            else
            {
                skillList = existMentor.MentorSkills.ToList();
            }
            
            skillList.Add(skills);
            
            var mentor = new MentorProjection()
            {
                Id = existMentor.Id,
                DocumentId = existMentor.DocumentId,
                Email = existMentor.Email,
                FullName = existMentor.FullName,
                Points = existMentor.Points,
                Role = existMentor.Role,
                Status = existMentor.Status,
                IsDeleted = existMentor.IsDeleted,
                MentorSkills = skillList
            };
            
            await _mentorRepository.ReplaceOneAsync(mentor);
        }
        
        await _cacheService.RemoveByPrefixAsync($"{nameof(Query.GetMentorsQuery)}", cancellationToken);
        
        return AbsrationShared.Result.Success();
    }
}