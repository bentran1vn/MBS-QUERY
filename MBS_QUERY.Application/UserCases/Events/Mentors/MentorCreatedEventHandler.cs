using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using DomainEventShared = MBS_CONTRACT.SHARE.Services.Mentors.DomainEvent;
using AbsrationShared = MBS_CONTRACT.SHARE.Abstractions.Shared;


namespace MBS_QUERY.Application.UserCases.Events.Mentors;

public class MentorCreatedEventHandler(IMongoRepository<MentorProjection> mentorRepository) : MBS_CONTRACT.SHARE.Abstractions.Messages.ICommandHandler<DomainEventShared.MentorCreated>
{
    public async Task<AbsrationShared.Result> Handle(DomainEventShared.MentorCreated request, CancellationToken cancellationToken)
    {
        var mentor = new MentorProjection
        {
            DocumentId = request.Id,
            Email = request.Email,
            FullName = request.FullName,
            Points = request.Points,
            Role = request.Role,
            Status = request.Status,
            IsDeleted = request.IsDeleted,
            CreatedOnUtc = request.CreatedOnUtc.DateTime,
            MentorSkills = [],
            MentorSlots = []
        };

        await mentorRepository.InsertOneAsync(mentor);

        return AbsrationShared.Result.Success();
    }
}