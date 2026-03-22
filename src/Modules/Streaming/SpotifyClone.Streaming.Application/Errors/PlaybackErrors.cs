using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Streaming.Application.Errors;

public static class PlaybackErrors
{
    public static readonly Error InvalidPlaybackContext = new(
        "Playlist.InvalidPlaybackContext",
        "The provided Playback Context is invalid.");

    public static readonly Error NotLoggedIn = new(
        "Playlist.NotLoggedIn",
        "User is not authenticated.");

    public static readonly Error NotFound = CommonErrors.NotFound(
        "PlaybackSession", "Playback session");
}
