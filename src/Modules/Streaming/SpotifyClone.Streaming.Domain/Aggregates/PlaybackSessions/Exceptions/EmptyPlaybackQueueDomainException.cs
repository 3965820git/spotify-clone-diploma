using SpotifyClone.Streaming.Domain.Exceptions;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Exceptions;

public sealed class EmptyPlaybackQueueDomainException(string message)
    : StreamingDomainExceptionBase(message);
