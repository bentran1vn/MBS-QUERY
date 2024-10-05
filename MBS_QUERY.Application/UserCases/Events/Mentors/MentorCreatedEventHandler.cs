using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using DomainEventShared = MBS_CONTRACT.SHARE.Services.Mentors.DomainEvent;
using AbsrationShared = MBS_CONTRACT.SHARE.Abstractions.Shared;


namespace MBS_QUERY.Application.UserCases.Events.Mentors;

public class MentorCreatedEventHandler : MBS_CONTRACT.SHARE.Abstractions.Messages.ICommandHandler<DomainEventShared.MentorCreated>
{
    private readonly IMongoRepository<MentorProjection> _mentorRepository;

    public MentorCreatedEventHandler(IMongoRepository<MentorProjection> mentorRepository)
    {
        _mentorRepository = mentorRepository;
    }

    public async Task<AbsrationShared.Result> Handle(DomainEventShared.MentorCreated request, CancellationToken cancellationToken)
    {
        var mentor = new MentorProjection()
        {
            DocumentId = request.Id,
            Email = request.Email,
            FullName = request.FullName,
            Points = request.Points,
            Role = request.Role,
            Status = request.Status,
            IsDeleted = request.IsDeleted,
            MentorSkills = new List<SkillProjection>()
        };
        
        await _mentorRepository.InsertOneAsync(mentor);
        
        return AbsrationShared.Result.Success();
    }
}