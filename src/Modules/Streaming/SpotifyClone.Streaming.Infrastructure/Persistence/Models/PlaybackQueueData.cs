namespace SpotifyClone.Streaming.Infrastructure.Persistence.Models;

public sealed record PlaybackQueueData(
    IEnumerable<Guid> CurrentQueue,
    IEnumerable<Guid> PreviousQueue,
    IEnumerable<Guid> OriginalQueue);
