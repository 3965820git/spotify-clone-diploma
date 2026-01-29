namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;

public sealed class InvalidGenderDomainException : AccountsDomainExceptionBase
{
    public InvalidGenderDomainException(string message)
        : base(message)
    {
    }
}
