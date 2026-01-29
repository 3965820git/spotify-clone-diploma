namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;

public sealed class InvalidBirthDateDomainException : AccountsDomainExceptionBase
{
    public InvalidBirthDateDomainException(string message)
        : base(message)
    {
    }
}
