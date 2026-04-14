using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Playlists.Application.Errors;

public static class CommonPlaylistsErrors
{
    public static readonly Error InvalidImageMetadata = new(
        "Image.InvalidMetadata",
        "Image metadata is invalid.");
}
