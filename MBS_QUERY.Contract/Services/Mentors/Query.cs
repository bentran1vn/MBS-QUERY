using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;

namespace MBS_QUERY.Contract.Services.Mentors;

public class Query
{
    public record GetAllMentorsQuery(string? searchTerm, int pageIndex, int pageSize) : IQuery<PagedResult<Response.GetAllMentorsResponse>>;
    
    public record GetProductByIdQuery(Guid Id) : IQuery<Response.GetMentorResponse>;
}