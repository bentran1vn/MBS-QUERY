using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Enumerations;

namespace MBS_QUERY.Contract.Services.Mentors;

public class Query
{
    public record GetMentorsQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.GetAllMentorsResponse>>;
    
    public record GetMentorQuery(Guid Id) : IQuery<Response.GetMentorResponse>;
}