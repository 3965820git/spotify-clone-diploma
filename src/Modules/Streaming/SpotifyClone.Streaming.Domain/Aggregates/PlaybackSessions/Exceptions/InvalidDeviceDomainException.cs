using SpotifyClone.Streaming.Domain.Exceptions;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Exceptions;

public sealed class InvalidDeviceDomainException(string message)
    : StreamingDomainExceptionBase(message);
