using MBS_QUERY.Contract.Abstractions.Messages;

namespace MBS_QUERY.Contract.Services.Mentors;

public static class DomainEvent
{
    public record MentorCreated(Guid IdEvent, Guid Id, string Email,
        string FullName, int Role, int Points, int Status,
        DateTimeOffset CreatedOnUtc, bool IsDeleted) : IDomainEvent, ICommand;
}