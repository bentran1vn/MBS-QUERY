namespace MBS_QUERY.Contract.Services.Groups;
public static class Reponse
{
    public record GroupDetailResponse
    {
        public string name { get; set; }
        public string Stack { get; set; }
        public string mentorName { get; set; }
        public Guid mentorId { get; set; }
        public string mentorEmail { get; set; }
        public string leaderName { get; set; }
        public string projectName { get; set; }
        public string projectDescription { get; set; }
        public double? BookingPoints { get; set; }
        public IReadOnlyCollection<Member> Members { get; set; }
    }

    public class Mentor
    {
    }

    public record Member
    {
        public Guid? UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }


    public record MemberDetail
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public double Points { get; set; }
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