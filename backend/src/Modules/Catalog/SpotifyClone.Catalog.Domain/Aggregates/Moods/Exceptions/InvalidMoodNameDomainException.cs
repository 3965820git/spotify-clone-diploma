using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Moods.Exceptions;

public sealed class InvalidMoodNameDomainException(string message)
    : CatalogDomainExceptionBase(message);
