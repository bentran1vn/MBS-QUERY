namespace MBS_QUERY.Contract.Services.Groups;

public static class Reponse
{
    public record GroupDetailResponse
    {
        public string name { get; set; }
        public string Stack { get; set; }
        public string mentorName { get; set; }
        public string leaderName { get; set; }
        public string projectName { get; set; }
        public Dictionary<string, string> Members { get; set; }
    }

    public record GroupResponse
    {
        public Guid GroupId { get; set; }
        public string name { get; set; }
        public string mentorName { get; set; }
        public string leaderName { get; set; }
        public string projectName { get; set; }
    }
}