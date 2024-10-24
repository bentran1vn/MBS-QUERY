namespace MBS_QUERY.Contract.Services.FeedBacks;
public static class Response
{
    public record FeedbackResponse
    {
        public Guid FeedbackId { get; init; }
        public string Content { get; init; }
        public string GroupName { get; init; }
        public int Rating { get; init; }
        public Guid ScheduleId { get; init; }
        public bool IsMentor { get; init; }
    }
}