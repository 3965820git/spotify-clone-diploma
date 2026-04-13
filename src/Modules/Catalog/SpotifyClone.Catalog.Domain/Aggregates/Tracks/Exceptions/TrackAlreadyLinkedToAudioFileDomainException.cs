using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Tracks.Exceptions;

public sealed class TrackAlreadyLinkedToAudioFileDomainException(string message)
    : CatalogDomainExceptionBase(message);
