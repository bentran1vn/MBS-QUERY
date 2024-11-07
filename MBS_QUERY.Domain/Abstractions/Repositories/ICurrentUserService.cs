namespace MBS_QUERY.Domain.Abstractions.Repositories;
public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserName { get; }
}