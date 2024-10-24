namespace MBS_QUERY.Contract.Services.Slots;
public static class Response
{
    public record SlotResponse
    {
        public Guid SlotId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }
        public bool IsOnline { get; set; }
        public string? Note { get; set; }
        public short? Month { get; set; }
        public bool IsBook { get; set; }
    }
    public record SlotGroupResponse
    {
        public DateOnly Date { get; set; }
        public List<SlotResponse> Slots { get; set; }
    }
}