using Carter;
using MBS_QUERY.Contract.Services.Users;
using MBS_QUERY.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MBS_QUERY.Presentation.APIs.User;

public class UserApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/user";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Users").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapGet("{index}/{email}", FindUserByEmail);
    }

    private static async Task<IResult> FindUserByEmail(ISender sender, [FromRoute] string email,int index=10)
    {
        var result = await sender.Send(new Query.FindUserByEmail(email, index));
        return result.IsSuccess ? Results.Ok(result) : Results.NotFound();
    }
    
}