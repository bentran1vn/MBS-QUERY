namespace MBS_QUERY.Contract.Services.Users;

public static class Response
{
    public record ProductResponse(Guid Id, string Name, decimal Price, string Description);
}