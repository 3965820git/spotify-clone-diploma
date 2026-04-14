using SpotifyClone.Accounts.Domain.Exceptions;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;

public sealed class InvalidAvatarImageDomainException(string message)
    : AccountsDomainExceptionBase(message);
