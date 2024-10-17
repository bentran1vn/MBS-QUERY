using Carter;
using MBS_QUERY.Contract.Services.Subjects;
using MBS_QUERY.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MBS_QUERY.Presentation.APIs.Subjects;

public class SubjectApi : ApiEndpoint,ICarterModule
{
    private const string BasePath = "/api/v{version:apiVersion}/subjects";
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Subjects").MapGroup(BasePath).HasApiVersion(1);
        gr1.MapGet(string.Empty, GetAllSubjects);
    }
    
    private static async Task<IResult> GetAllSubjects(ISender sender,
        int pageIndex = 1,
        int pageSize = 5)
    {
        var result = await sender.Send(new Query.GetSubjectsQuery(
            pageIndex, pageSize));
        
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}