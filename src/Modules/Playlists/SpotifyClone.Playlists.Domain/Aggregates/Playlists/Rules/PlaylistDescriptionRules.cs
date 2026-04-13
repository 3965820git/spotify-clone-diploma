using SpotifyClone.Playlists.Domain.Aggregates.Playlists.Exceptions;

namespace SpotifyClone.Playlists.Domain.Aggregates.Playlists.Rules;

public static class PlaylistDescriptionRules
{
    public const short MaxLength = 300;

    public static void Validate(string? description)
    {
        if (description?.Length > MaxLength)
        {
            throw new InvalidPlaylistMetadataDomainException(
                $"Description exceeds maximum length of {MaxLength} characters.");
        }
    }
}
