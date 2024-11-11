using MBS_CONTRACT.SHARE.Abstractions.Messages;
using MBS_CONTRACT.SHARE.Services.MentorSkills;
using MBS_QUERY.Application.Abstractions;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using Query = MBS_QUERY.Contract.Services.Mentors.Query;

namespace MBS_QUERY.Application.UserCases.Events.MentorSkills;
using DomainEventShared = DomainEvent;
using AbsrationShared = MBS_CONTRACT.SHARE.Abstractions.Shared;

public class MentorSkillsCreatedHandler : ICommandHandler<DomainEvent.MentorSkillsCreated>
{
    private readonly ICacheService _cacheService;
    private readonly IMongoRepository<MentorProjection> _mentorRepository;

    public MentorSkillsCreatedHandler(IMongoRepository<MentorProjection> mentorRepository, ICacheService cacheService)
    {
        _mentorRepository = mentorRepository;
        _cacheService = cacheService;
    }

    public async Task<AbsrationShared.Result> Handle(DomainEvent.MentorSkillsCreated request,
        CancellationToken cancellationToken)
    {
        var existMentor = await _mentorRepository.FindOneAsync(p => p.DocumentId.Equals(request.MentorId));

        if (existMentor is null) return AbsrationShared.Result.Success();

        var newSkill = new SkillProjection
        {
            DocumentId = request.Id,
            SkillCetificates = request.Certificates.Select(x => new CertificateProjection
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

        // Initialize skill list if MentorSkills is empty
        var skillList = existMentor.MentorSkills?.ToList() ?? new List<SkillProjection>();

        // Try to find an existing skill with the same DocumentId
        var existingSkill = skillList.FirstOrDefault(x => x.DocumentId.Equals(newSkill.DocumentId));

        if (existingSkill == null)
        {
            // If skill does not exist, add it to the list
            skillList.Add(newSkill);
        }
        else
        {
            // If skill exists, merge the new certificates with existing ones
            var newCertificates = newSkill.SkillCetificates
                .Where(newCert => !existingSkill.SkillCetificates.Any(existingCert => existingCert.Name.Equals(newCert.Name)))
                .ToList();

            existingSkill.SkillCetificates.AddRange(newCertificates);
        }

        existMentor.MentorSkills = skillList;
        await _mentorRepository.ReplaceOneAsync(existMentor);

        await _cacheService.RemoveByPrefixAsync($"{nameof(Query.GetMentorsQuery)}", cancellationToken);

        return AbsrationShared.Result.Success();
    }
}