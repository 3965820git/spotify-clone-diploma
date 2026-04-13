using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Genres.Exceptions;

public sealed class InvalidGenreNameDomainException(string message)
    : CatalogDomainExceptionBase(message);
