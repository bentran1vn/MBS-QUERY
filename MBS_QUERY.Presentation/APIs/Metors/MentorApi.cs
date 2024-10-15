using Carter;
using MBS_COMMAND.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Mentors;
using MBS_QUERY.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MBS_QUERY.Presentation.APIs.Metors;

public class MentorApi: ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/mentors";
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group1 = app.NewVersionedApi("Mentors")
            .MapGroup(BaseUrl).HasApiVersion(1);
        
        group1.MapGet(string.Empty, GetAllMentors);
    }
    
    public static async Task<IResult> GetAllMentors(ISender sender,
        string? serchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await sender.Send(new Query.GetAllMentorsQuery(serchTerm,
            pageIndex, pageSize));
        return Results.Ok(result);
    }
}