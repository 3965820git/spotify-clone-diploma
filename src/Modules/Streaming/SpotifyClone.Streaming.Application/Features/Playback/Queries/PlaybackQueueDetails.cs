namespace SpotifyClone.Streaming.Application.Features.Playback.Queries;

public sealed record PlaybackQueueDetails(
    Guid CurrentTrackId,
    IEnumerable<Guid> TracksInQueue);
