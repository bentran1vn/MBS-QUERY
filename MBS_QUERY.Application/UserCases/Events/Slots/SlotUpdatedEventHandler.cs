using MBS_CONTRACT.SHARE.Abstractions.Messages;
using MBS_CONTRACT.SHARE.Abstractions.Shared;
using MBS_CONTRACT.SHARE.Services.Slots;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;


namespace MBS_QUERY.Application.UserCases.Events.Slots;
public class SlotUpdatedEventHandler(IMongoRepository<SlotProjection> mongoRepository)
    : ICommandHandler<DomainEvent.SlotUpdated>
{
    public async Task<Result> Handle(DomainEvent.SlotUpdated request, CancellationToken cancellationToken)
    {
        var slot = await mongoRepository.FindOneAsync(x => x.SlotId == request.Slot.SlotId);
        slot.MentorId = request.Slot.MentorId;
        slot.StartTime = request.Slot.StartTime;
        slot.EndTime = request.Slot.EndTime;
        slot.Date = request.Slot.Date;
        slot.IsOnline = request.Slot.IsOnline;
        slot.Note = request.Slot.Note;
        slot.Month = request.Slot.Month;
        slot.IsBook = request.Slot.IsBook;
        slot.IsDeleted = request.Slot.IsDeleted;
        await mongoRepository.ReplaceOneAsync(slot);
        return Result.Success();
    }
}