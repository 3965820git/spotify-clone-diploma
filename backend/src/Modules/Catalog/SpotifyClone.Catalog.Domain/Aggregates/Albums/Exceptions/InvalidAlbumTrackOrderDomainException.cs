using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Albums.Exceptions;

public sealed class InvalidAlbumTrackOrderDomainException(string message)
    : CatalogDomainExceptionBase(message);
