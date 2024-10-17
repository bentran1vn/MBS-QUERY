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
        public IReadOnlyCollection<Member> Members { get; set; }
    }

    public record Member
    {
        public string Email { get; set; }
        public string FullName { get; set; }
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