using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Genres.Exceptions;

public sealed class InvalidGenreCoverImageDomainException(string message)
    : CatalogDomainExceptionBase(message);
