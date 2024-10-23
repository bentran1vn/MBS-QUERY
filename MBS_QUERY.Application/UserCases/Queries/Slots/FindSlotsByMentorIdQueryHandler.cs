using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Slots;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;
using MBS_QUERY.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MBS_QUERY.Application.UserCases.Queries.Slots;
public class FindSlotsByMentorIdQueryHandler(IMongoRepository<SlotProjection> slotRepository)
    : IQueryHandler<Query.FindSlotsByMentorId, List<Response.SlotResponse>>
{
    public async Task<Result<List<Response.SlotResponse>>> Handle(Query.FindSlotsByMentorId request,
        CancellationToken cancellationToken)
    {
        var slots = slotRepository.FilterBy(x => x.MentorId.Equals(request.MentorId)).ToAsyncEnumerable();
        var result = await slots
            .Select(x => new Response.SlotResponse
            {
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Date = x.Date,
                IsOnline = x.IsOnline,
                Note = x.Note,
                Month = x.Month,
                IsBook = x.IsBook
            })
            .ToListAsync(cancellationToken);
        return Result.Success(result);
    }
}