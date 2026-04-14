using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Tracks.Exceptions;

public sealed class InvalidTrackGenresDomainException(string message)
    : CatalogDomainExceptionBase(message);
