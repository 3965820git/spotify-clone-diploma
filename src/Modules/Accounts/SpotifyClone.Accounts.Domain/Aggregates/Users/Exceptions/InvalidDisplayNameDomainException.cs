using SpotifyClone.Accounts.Domain.Exceptions;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;

public sealed class InvalidDisplayNameDomainException(string message)
    : AccountsDomainExceptionBase(message);
