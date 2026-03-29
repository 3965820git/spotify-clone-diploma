using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Streaming.Application.Errors;

public static class PlaybackErrors
{
    public static readonly Error InvalidContext = new(
        "Playback.InvalidContext",
        "The provided Playback Context is invalid.");

    public static readonly Error EmptyQueue = new(
        "Playback.EmptyQueue",
        "Playback Queue must have at least one track.");

    public static readonly Error InvalidDevice = new(
        "Playback.InvalidDevice",
        "The provided device is invalid.");

    public static readonly Error InvalidPlayedAtDate = new(
        "Playback.InvalidPlayedAtDate",
        "The specified played at date is invalid.");

    public static readonly Error NotLoggedIn = new(
        "Playback.NotLoggedIn",
        "User is not authenticated.");

    public static readonly Error SessionNotFound = CommonErrors.NotFound(
        "PlaybackSession", "Playback session");

    public static readonly Error HistoryEntryNotFound = CommonErrors.NotFound(
        "PlaybackHistoryEntry", "Playback history entry");
}
