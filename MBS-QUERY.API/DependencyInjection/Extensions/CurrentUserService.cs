using System.Security.Claims;
using MBS_QUERY.Domain.Abstractions.Repositories;

namespace MBS_QUERY.API.DependencyInjection.Extensions;
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly ClaimsPrincipal? _claimsPrincipal = httpContextAccessor?.HttpContext?.User;

    public string? UserId => _claimsPrincipal?.FindFirstValue("UserId");

    public string? UserName => _claimsPrincipal?.FindFirstValue(ClaimTypes.Name);
}