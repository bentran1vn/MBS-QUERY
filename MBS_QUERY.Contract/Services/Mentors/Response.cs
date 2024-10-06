namespace MBS_QUERY.Contract.Services.Mentors;

public static class Response
{
    public record GetAllMentorsResponse()
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
    };
    public record GetMentorResponse(Guid Id, string Name, decimal Price, string Description);
}
