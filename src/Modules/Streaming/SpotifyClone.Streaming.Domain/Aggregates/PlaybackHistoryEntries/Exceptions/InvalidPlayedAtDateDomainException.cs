using SpotifyClone.Streaming.Domain.Exceptions;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries.Exceptions;

public sealed class InvalidPlayedAtDateDomainException(string message)
    : StreamingDomainExceptionBase(message);
