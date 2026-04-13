using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Streaming.Domain.Exceptions;

public abstract class StreamingDomainExceptionBase(string message)
    : DomainExceptionBase(message);
