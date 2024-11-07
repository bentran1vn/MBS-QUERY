using Carter;
using MBS_QUERY.Contract.Services.Sumaries;
using MBS_QUERY.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MBS_QUERY.Presentation.APIs.Sumaries;
public class SumaryApi : ApiEndpoint, ICarterModule
{
    private const string BasePath = "/api/v{version:apiVersion}/sumaries";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Sumaries").MapGroup(BasePath).HasApiVersion(1);
        gr1.MapGet(string.Empty, GetAllSumaries);
    }

    private async Task<IResult> GetAllSumaries(ISender sender)
    {
        var result = await sender.Send(new Query.GetSumariesQuery());

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}