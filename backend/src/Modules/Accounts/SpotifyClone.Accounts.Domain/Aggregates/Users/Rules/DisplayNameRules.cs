using SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Rules;

public static class DisplayNameRules
{
    public const short MaxLength = 32;

    public static void Validate(string displayName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(displayName);

        if (displayName.Length > MaxLength)
        {
            throw new InvalidDisplayNameDomainException(
                $"Display name exceeds maximum length of {MaxLength} characters.");
        }

        if (displayName.Any(char.IsControl))
        {
            throw new InvalidDisplayNameDomainException(
                "Display name contains invalid characters.");
        }
    }
}
