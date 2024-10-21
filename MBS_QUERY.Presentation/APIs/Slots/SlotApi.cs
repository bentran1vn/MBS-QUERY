using Carter;
using MBS_QUERY.Contract.Services.Slots;
using MBS_QUERY.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MBS_QUERY.Presentation.APIs.Slots;
public class SlotApi :ApiEndpoint,ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/slots";
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group1 = app.NewVersionedApi("Slots")
            .MapGroup(BaseUrl).HasApiVersion(1);
       group1.MapGet("{mentorId:Guid}",GetSlotsByMentorId);
    }
    private static async Task<IResult> GetSlotsByMentorId(ISender sender,Guid mentorId)
    {
        var result = await sender.Send(new Query.FindSlotsByMentorId(mentorId));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}