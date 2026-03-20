namespace SpotifyClone.Streaming.Domain.Exceptions;

public sealed class InvalidPlaybackContextDomainException(
    string message)
    : StreamingDomainExceptionBase(message);
