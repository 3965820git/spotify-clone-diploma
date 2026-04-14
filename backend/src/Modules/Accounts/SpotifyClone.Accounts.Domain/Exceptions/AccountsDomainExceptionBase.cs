using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Accounts.Domain.Exceptions;

public abstract class AccountsDomainExceptionBase(string message)
    : DomainExceptionBase(message);
