using SpotifyClone.Playlists.Domain.Aggregates.Playlists.Exceptions;

namespace SpotifyClone.Playlists.Domain.Aggregates.Playlists.Rules;

public static class PlaylistNameRules
{
    public const short MaxLength = 100;

    public static void Validate(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (name.Length > MaxLength)
        {
            throw new InvalidPlaylistMetadataDomainException($"Name exceeds maximum length of {MaxLength} characters.");
        }
    }
}
