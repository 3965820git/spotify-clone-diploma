using SpotifyClone.Catalog.Domain.Aggregates.Artists.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Artists.Rules;

public static class ArtistBioRules
{
    public const short MaxLength = 1500;

    public static void Validate(string? bio)
    {
        if (bio is null)
        {
            return;
        }

        if (bio.Length > MaxLength)
        {
            throw new InvalidArtistBioDomainException($"Title exceeds maximum length of {MaxLength} characters.");
        }
    }
}
