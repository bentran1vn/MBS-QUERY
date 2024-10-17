namespace MBS_QUERY.Contract.Services.Schedule;

public static class Response
{
    public record ScheduleResponse
    {
        public string GroupName { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }
        public bool IsFeedback { get; set; }
    }
}