using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Slots;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MBS_QUERY.Application.UserCases.Queries.Slots;
public class FindSlotsByMentorIdQueryHandler(IRepositoryBase<Slot, Guid> slotRepository)
    : IQueryHandler<Query.FindSlotsByMentorId, List<Response.SlotResponse>>
{
    // private readonly IMongoRepository<SlotProjection> _slotRepository;
    //
    // public FindSlotsByMentorIdQueryHandler(IMongoRepository<SlotProjection> slotRepository)
    // {
    //     _slotRepository = slotRepository;
    // }
    // public Task<Result<List<Response.SlotResponse>>> Handle(Query.FindSlotsByMentorId request, CancellationToken cancellationToken)
    // {
    //     var slots = _slotRepository.FilterBy(x=>x.MentorId.Equals(request.MentorId)).ToList();
    //     var result = slots.Select(x => new Response.SlotResponse
    //     {
    //         StartTime = x.StartTime,
    //         EndTime = x.EndTime,
    //         Date = x.Date,
    //         IsOnline = x.IsOnline,
    //         Note = x.Note,
    //         Month = x.Month,
    //         IsBook = x.IsBook
    //     }).ToList();
    //     return Task.FromResult(Result.Success(result));
    //     
    // }

    public async Task<Result<List<Response.SlotResponse>>> Handle(Query.FindSlotsByMentorId request,
        CancellationToken cancellationToken)
    {
        var slotResponses = await slotRepository.FindAll(x => x.MentorId.Equals(request.MentorId))
            .AsAsyncEnumerable()
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
        return Result.Success(slotResponses);
    }
}