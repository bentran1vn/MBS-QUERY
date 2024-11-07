using Carter;
using MBS_QUERY.Contract.Services.Skills;
using MBS_QUERY.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MBS_QUERY.Presentation.APIs.Skills;
public class SkillApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/skills";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group1 = app.NewVersionedApi("Skills")
            .MapGroup(BaseUrl).HasApiVersion(1);

        group1.MapGet(string.Empty, GetAllSkills);
    }

    public static async Task<IResult> GetAllSkills(ISender sender,
        int pageIndex = 1,
        int pageSize = 5)
    {
        var result = await sender.Send(new Query.GetSkillsQuery(
            pageIndex, pageSize));

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }
}