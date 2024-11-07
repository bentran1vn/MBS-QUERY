namespace MBS_QUERY.Contract.Services.Mentors;
public static class Response
{
    public interface MentorResponse
    {
        public Guid Id { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
        public int Point { set; get; }
        public DateTime CreatedOnUtc { set; get; }
    }

    public record GetAllMentorsResponse : MentorResponse
    {
        public IReadOnlyCollection<string> Skills { set; get; }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Point { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }

    public record GetMentorResponse : MentorResponse
    {
        public IReadOnlyCollection<Skill> Skills { set; get; } = default!;
        public IReadOnlyCollection<Slot> Slots { set; get; } = default!;
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int Point { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }

    public class Slot
    {
        // public Guid? MentorId { get; set; }
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

    public record ShowListMentorResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTimeOffset CreatedAtUtc { get; set; }
    }
}