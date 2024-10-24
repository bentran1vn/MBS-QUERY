using System.Linq.Expressions;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Slots;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Documents;

namespace MBS_QUERY.Application.UserCases.Queries.Slots;
public class FindSlotsByMentorIdQueryHandler(IMongoRepository<SlotProjection> slotRepository)
    : IQueryHandler<Query.FindSlotsByMentorId, List<Response.SlotGroupResponse>>
{
    public async Task<Result<List<Response.SlotGroupResponse>>> Handle(Query.FindSlotsByMentorId request,
        CancellationToken cancellationToken)
    {
        var mentorId = request.MentorId.ToString();
        DateOnly? startDate = null;
        DateOnly? endDate = null;

        if (DateOnly.TryParse(request.Date, out var parsedDateOnly))
        {
            var dateRange = ExtensionMethod.GetWeekDates.Get(parsedDateOnly);
            startDate = dateRange[0];
            endDate = dateRange[6];
        }

        // Simplified filter expression
        Expression<Func<SlotProjection, bool>> filter = x =>
            (string.IsNullOrEmpty(mentorId) || x.MentorId.Equals(request.MentorId)) &&
            (!startDate.HasValue || (x.Date >= startDate && x.Date <= endDate));

        // Assuming the repository supports async querying
        var result = slotRepository.FilterBy(filter)
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
            })
            .GroupBy(x => x.Date)
            .Select(g => new Response.SlotGroupResponse
            {
                Date = g.Key,
                Slots = g.ToList()
            })
            .ToList();


        return Result.Success(result);
    }
}