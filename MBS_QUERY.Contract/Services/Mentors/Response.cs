namespace MBS_QUERY.Contract.Services.Mentors;

public static class Response
{
    public record GetAllMentorsResponse
    {
        public Guid Id { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
        public int Point { set; get; }
        public DateTime CreatedOnUtc { set; get; }
        public IReadOnlyCollection<Skill> Skills { set; get; }
    };
    public class Slot
    {
        public Guid? MentorId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }
        public bool IsOnline { get; set; }
        public string? Note { get; set; }
        public short? Month { get; set; }
        public bool IsBook { get; set; }
    }
    public class Skill
    {
        public string SkillName { set; get; }
        public string SkillDesciption { set; get; }
        public string SkillCategoryType { set; get; }
        public DateTime CreatedOnUtc { set; get; }
        public IReadOnlyCollection<Cetificate> Cetificates { set; get; }
    }
    
    public class Cetificate
    {
        public string CetificateName { set; get; }
        public string CetificateDesciption { set; get; }
        public string CetificateImageUrl { set; get; }
        public DateTime CreatedOnUtc { set; get; }
    }
    
    public record GetMentorResponse(Guid Id, string Name, decimal Price, string Description);
}
