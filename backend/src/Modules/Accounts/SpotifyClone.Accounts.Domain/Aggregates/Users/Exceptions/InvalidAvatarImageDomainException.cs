namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;

public sealed class InvalidAvatarImageDomainException : AccountsDomainExceptionBase
{
    public InvalidAvatarImageDomainException(string message)
        : base(message)
    {
    }
}
