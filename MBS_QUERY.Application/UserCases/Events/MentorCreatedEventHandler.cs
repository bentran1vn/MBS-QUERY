using MBS_COMMAND.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Services.Mentors;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;

namespace MBS_QUERY.Application.UserCases.Events;

public class MentorCreatedEventHandler : ICommandHandler<DomainEvent.MentorCreated>
{
    private readonly IMongoRepository<MentorProjection> _mentorRepository;

    public MentorCreatedEventHandler(IMongoRepository<MentorProjection> mentorRepository)
    {
        _mentorRepository = mentorRepository;
    }


    public async Task<Result> Handle(DomainEvent.MentorCreated request, CancellationToken cancellationToken)
    {
        var mentor = new MentorProjection()
        {
            Email = request.Email,
            FullName = request.FullName,
            Points = request.Points,
            Role = request.Role,
            Status = request.Status,
            IsDeleted = request.IsDeleted,
        };
        
        await _mentorRepository.InsertOneAsync(mentor);
        
        return Result.Success();
    }
}