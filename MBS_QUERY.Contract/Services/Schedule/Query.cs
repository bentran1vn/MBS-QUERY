using MBS_QUERY.Contract.Abstractions.Messages;

namespace MBS_QUERY.Contract.Services.Schedule;

public static class Query
{
    public record GetAllBookedScheduleQuery : IQuery<List<Response.ScheduleResponse>>;
}