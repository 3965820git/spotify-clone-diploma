using SpotifyClone.Accounts.Domain.Exceptions;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;

public sealed class InvalidBirthDateDomainException(string message)
    : AccountsDomainExceptionBase(message);
