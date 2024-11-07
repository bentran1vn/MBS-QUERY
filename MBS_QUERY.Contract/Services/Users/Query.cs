using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Services.Groups;

namespace MBS_QUERY.Contract.Services.Users;
public static class Query
{
    public record FindUserByEmail(string Email, int Role, int Index = 10) : IQuery<List<Reponse.Member>>;

    public record FindUserById(Guid Id) : IQuery<Reponse.MemberDetail>;
}