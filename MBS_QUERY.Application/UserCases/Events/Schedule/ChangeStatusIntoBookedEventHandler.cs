using MBS_CONTRACT.SHARE.Abstractions.Messages;
using MBS_CONTRACT.SHARE.Abstractions.Shared;
using MBS_CONTRACT.SHARE.Services.Slots;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;

namespace MBS_QUERY.Application.UserCases.Events.Schedule;
public class ChangeStatusIntoBookedEventHandler(IMongoRepository<SlotProjection> mongoRepository)
    : ICommandHandler<DomainEvent.ChangeSlotStatusInToBooked>
{
    public async Task<Result> Handle(DomainEvent.ChangeSlotStatusInToBooked request,
        CancellationToken cancellationToken)
    {
        var slot = await mongoRepository.FindOneAsync(x => x.SlotId == request.SlotId);
        slot.IsBook = true;
        await mongoRepository.ReplaceOneAsync(slot);
        return Result.Success();
    }
}