using MBS_QUERY.Contract.Abstractions.Messages;

namespace MBS_QUERY.Contract.Services.Users;

public static class Command
{
    public record CreateProductCommand(string Name, decimal Price, string Description) : ICommand;

    public record UpdateProductCommand(Guid Id, string Name, decimal Price, string Description) : ICommand;

    public record DeleteProductCommand(Guid Id) : ICommand;
}
