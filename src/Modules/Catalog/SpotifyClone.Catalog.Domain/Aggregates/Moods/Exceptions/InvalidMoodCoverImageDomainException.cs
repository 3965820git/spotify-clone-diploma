using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Moods.Exceptions;

public sealed class InvalidMoodCoverImageDomainException(string message)
    : CatalogDomainExceptionBase(message);
