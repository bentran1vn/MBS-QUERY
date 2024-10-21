using MBS_QUERY.Contract.Abstractions.Messages;

namespace MBS_QUERY.Contract.Services.Slots;
public static class Query
{
    public record FindSlotsByMentorId(Guid MentorId) : IQuery<List<Response.SlotResponse>>;
}