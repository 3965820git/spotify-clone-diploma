using SpotifyClone.Streaming.Domain.Exceptions;

namespace SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Exceptions;

public sealed class InvalidAudioFormatDomainException(string message)
    : StreamingDomainExceptionBase(message);
