using MBS_QUERY.Contract.Abstractions.Messages;

namespace MBS_QUERY.Contract.Services.Slots;
public static class Query
{
    public record FindSlotsByMentorId(Guid MentorId,string? Date) : IQuery<List<Response.SlotResponse>>;
}