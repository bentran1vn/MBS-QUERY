using Carter;
using MBS_QUERY.Contract.Extensions;
using MBS_QUERY.Contract.Services.Mentors;
using MBS_QUERY.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MBS_QUERY.Presentation.APIs.Mentors;
public class MentorApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/mentors";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group1 = app.NewVersionedApi("Mentors")
            .MapGroup(BaseUrl).HasApiVersion(1);

        group1.MapGet(string.Empty, GetAllMentors).RequireAuthorization();
        group1.MapGet("{mentorId}", GetMentorsById).RequireAuthorization();
        group1.MapGet("approve", ShowListMentor);
    }

    private static async Task<IResult> ShowListMentor(ISender sender)
    {
        var result = await sender.Send(new Query.ShowListMentorQuery());

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> GetMentorsById(ISender sender,
        Guid mentorId)
    {
        var result = await sender.Send(new Query.GetMentorQuery(mentorId));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> GetAllMentors(ISender sender,
        string? serchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetMentorsQuery(serchTerm,
            sortColumn, SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            pageIndex, pageSize));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}