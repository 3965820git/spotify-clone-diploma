using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;

public abstract class AccountsDomainExceptionBase : DomainExceptionBase
{
    protected AccountsDomainExceptionBase(string message)
        : base(message)
    {
    }
}
