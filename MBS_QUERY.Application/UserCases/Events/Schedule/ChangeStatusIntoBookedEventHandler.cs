using MBS_CONTRACT.SHARE.Abstractions.Messages;
using MBS_CONTRACT.SHARE.Abstractions.Shared;
using MBS_CONTRACT.SHARE.Services.Slots;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;


namespace MBS_QUERY.Application.UserCases.Events.Schedule;
public class ChangeStatusIntoBookedEventHandler : ICommandHandler<DomainEvent.ChangeSlotStatusInToBooked>
{
    private readonly IMongoRepository<SlotProjection> _mongoRepository;

    public ChangeStatusIntoBookedEventHandler(IMongoRepository<SlotProjection> mongoRepository)
    {
        _mongoRepository = mongoRepository;
    }

    public async Task<Result> Handle(DomainEvent.ChangeSlotStatusInToBooked request,
        CancellationToken cancellationToken)
    {
        var slot = await _mongoRepository.FindOneAsync(x => x.SlotId == request.SlotId);
        slot.IsBook = true;
        await _mongoRepository.ReplaceOneAsync(slot);
        return Result.Success();
    }
}