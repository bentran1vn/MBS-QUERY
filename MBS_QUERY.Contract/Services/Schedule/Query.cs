using MBS_QUERY.Contract.Abstractions.Messages;

namespace MBS_QUERY.Contract.Services.Schedule;

public class Query
{
    public record GetAllBookedScheduleQuery(string DateOnly) : IQuery<List<Response.ScheduleResponse>>;
}