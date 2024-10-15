using MBS_QUERY.Contract.Abstractions.Messages;

namespace MBS_QUERY.Contract.Services.Mentors;

public class Query
{
    public record GetAllMentorsQuery() : IQuery<List<Response.GetAllMentorsResponse>>;
    public record GetAllMentorSlotQuery(Guid MentorId) : IQuery<List<Response.GetAllMentorsResponse>>;
    public record GetProductByIdQuery(Guid Id) : IQuery<Response.GetMentorResponse>;
}