using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Tracks.Exceptions;

public sealed class TrackAlreadyAttachedToAnAlbumDomainException(string message)
    : CatalogDomainExceptionBase(message);
