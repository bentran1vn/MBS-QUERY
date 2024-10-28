using Carter;
using MBS_QUERY.Contract.Services.Schedule;
using MBS_QUERY.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MBS_QUERY.Presentation.APIs.Schedules;
public class ScheduleApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/schedules";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Schedules").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapGet(string.Empty, GetAllSchedules).RequireAuthorization();
        gr1.MapGet("mentor", GetAllSchedulesOfMentor).RequireAuthorization();
    }

    private static async Task<IResult> GetAllSchedules(ISender sender)
    {
        var result = await sender.Send(new Query.GetAllBookedScheduleQuery());

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> GetAllSchedulesOfMentor(ISender sender)
    {
        var result = await sender.Send(new Query.GetAllBookedScheduleOfMentorQuery());

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}