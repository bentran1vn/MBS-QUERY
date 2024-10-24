using MBS_CONTRACT.SHARE.Abstractions.Shared;
using MBS_CONTRACT.SHARE.Services.Users;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MongoDB.Bson;
using static MBS_CONTRACT.SHARE.Abstractions.Shared.Result;

namespace MBS_QUERY.Application.UserCases.Events.Mentors;
public class MentorSlotCreatedEventHandler(IMongoRepository<SlotProjection> mongoRepository)
    : MBS_CONTRACT.SHARE.Abstractions.Messages.ICommandHandler<DomainEvent.MentorSlotCreated>
{
    public async Task<Result> Handle(DomainEvent.MentorSlotCreated request, CancellationToken cancellationToken)
    {
        var slotProjection = request.Slots.Select(x => new SlotProjection()
        {
            SlotId = x.SlotId,
            DocumentId = x.Id,
            MentorId = x.MentorId,
            Date = x.Date,
            StartTime = x.StartTime,
            EndTime = x.EndTime,
            IsOnline = x.IsOnline,
            Note = x.Note,
            Month = x.Month,
            IsBook = x.IsBook,
        }).ToList();
        await mongoRepository.InsertManyAsync(slotProjection);
        return Success();
    }
}