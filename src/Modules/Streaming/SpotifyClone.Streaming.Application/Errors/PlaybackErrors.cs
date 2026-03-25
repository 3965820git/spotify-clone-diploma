using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Streaming.Application.Errors;

public static class PlaybackErrors
{
    public static readonly Error InvalidPlaybackContext = new(
        "Playlist.InvalidPlaybackContext",
        "The provided Playback Context is invalid.");

    public static readonly Error EmptyQueue = new(
        "Playlist.EmptyQueue",
        "Playback Queue must have at least one track.");

    public static readonly Error InvalidDevice = new(
        "Playlist.InvalidDevice",
        "The provided device is invalid.");

    public static readonly Error NotLoggedIn = new(
        "Playlist.NotLoggedIn",
        "User is not authenticated.");

    public static readonly Error NotFound = CommonErrors.NotFound(
        "PlaybackSession", "Playback session");
}
