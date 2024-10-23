﻿using Carter;
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
        gr1.MapGet("get-all-booked/{date}", GetAllSchedules).WithSummary("must be dd/MM/yyyy format").RequireAuthorization();
    }

    private static async Task<IResult> GetAllSchedules(ISender sender, string date)
    {
        var result = await sender.Send(new Query.GetAllBookedScheduleQuery(date));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}