using SpotifyClone.Catalog.Domain.Aggregates.Genres.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Genres.Rules;

public static class GenreNameRules
{
    public const int MaxLength = 30;

    public static void Validate(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (name.Length > MaxLength)
        {
            throw new InvalidGenreNameDomainException($"Name cannot exceed {MaxLength} characters.");
        }
    }
}
