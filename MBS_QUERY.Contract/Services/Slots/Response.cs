namespace MBS_QUERY.Contract.Services.Slots;
public static class Response
{
    public record SlotResponse
    {
        
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }
        public bool IsOnline { get; set; }
        public string? Note { get; set; }
        public short? Month { get; set; }
        public bool IsBook { get; set; }
    }
}