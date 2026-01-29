namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;

public sealed class InvalidDisplayNameDomainException : AccountsDomainExceptionBase
{
    public InvalidDisplayNameDomainException(string message)
        : base(message)
    {
    }
}
