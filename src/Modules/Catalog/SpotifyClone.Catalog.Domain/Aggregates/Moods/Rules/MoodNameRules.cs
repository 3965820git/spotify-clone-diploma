using SpotifyClone.Catalog.Domain.Aggregates.Moods.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Moods.Rules;

public static class MoodNameRules
{
    public const int MaxLength = 30;

    public static void Validate(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (name.Length > MaxLength)
        {
            throw new InvalidMoodNameDomainException($"Name cannot exceed {MaxLength} characters.");
        }
    }
}
