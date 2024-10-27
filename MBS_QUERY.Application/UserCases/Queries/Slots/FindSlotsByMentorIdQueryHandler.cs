using System.Linq.Expressions;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Slots;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;

namespace MBS_QUERY.Application.UserCases.Queries.Slots;
public class FindSlotsByMentorIdQueryHandler(IMongoRepository<SlotProjection> slotRepository)
    : IQueryHandler<Query.FindSlotsByMentorId, List<Response.SlotResponse>>
{
    public async Task<Result<List<Response.SlotResponse>>> Handle(Query.FindSlotsByMentorId request,
        CancellationToken cancellationToken)
    {
        var slots =
            request.Date != null && DateOnly.TryParse(request.Date, out var parsedDateOnly)
                ? slotRepository.FilterBy(x =>
                        x.MentorId.Equals(request.MentorId) && x.Date == parsedDateOnly && !x.IsDeleted)
                    .ToAsyncEnumerable()
                : slotRepository.FilterBy(x => x.MentorId.Equals(request.MentorId) && !x.IsDeleted).ToAsyncEnumerable();

        var result = await slots
            .Select(x => new Response.SlotResponse
            {
                SlotId = x.SlotId,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Date = x.Date,
                IsOnline = x.IsOnline,
                Note = x.Note,
                Month = x.Month,
                IsBook = x.IsBook
            }).ToListAsync(cancellationToken);
        ;


        return Result.Success(result);
    }
}