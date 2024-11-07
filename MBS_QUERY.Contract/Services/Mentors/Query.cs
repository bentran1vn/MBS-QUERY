using System.Text;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Enumerations;

namespace MBS_QUERY.Contract.Services.Mentors;
public class Query
{
    public record GetMentorsQuery(
        string? SearchTerm,
        string? SortColumn,
        SortOrder? SortOrder,
        int PageIndex,
        int PageSize) : IQuery<PagedResult<Response.GetAllMentorsResponse>>, ICacheable
    {
        public bool BypassCache => false;
        public int SlidingExpirationInMinutes => 30;
        public int AbsoluteExpirationInMinutes => 60;

        public string CacheKey
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append($"{nameof(GetMentorsQuery)}");
                if (SearchTerm != null) builder.Append($"-SearchTerm:{SearchTerm}");
                builder.Append($"-Sort:{SortColumn}:{SortOrder}");
                builder.Append($"-Page:{PageIndex}:{PageSize}");
                return builder.ToString();
            }
        }
    }

    public record GetMentorQuery(Guid Id) : IQuery<Response.GetMentorResponse>;


    public record ShowListMentorQuery : IQuery<List<Response.ShowListMentorResponse>>;
}