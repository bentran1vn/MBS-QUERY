using Carter;
using MBS_QUERY.Contract.Services.Groups;
using MBS_QUERY.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MBS_QUERY.Presentation.APIs.Group;
public class GroupApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/groups";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Groups").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapGet(string.Empty, GetGroup).RequireAuthorization();
        gr1.MapGet("{groupId}", GroupDetail);
    }

    private static async Task<IResult> GetGroup(ISender sender)
    {
        var result = await sender.Send(new Query.GetGroupsQuery());
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> GroupDetail(ISender sender, [FromRoute] Guid groupId)
    {
        var result = await sender.Send(new Query.GetGroupDetail(groupId));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}