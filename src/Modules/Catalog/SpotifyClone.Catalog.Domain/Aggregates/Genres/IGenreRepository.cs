using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;

namespace SpotifyClone.Catalog.Domain.Aggregates.Genres;

public interface IGenreRepository
{
    Task<bool> IsNameUniqueAsync(
        string name,
        CancellationToken cancellationToken = default);

    Task<Genre?> GetByIdAsync(
        GenreId id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Genre>> GetByIdsAsync(
        IEnumerable<GenreId> ids,
        CancellationToken cancellationToken = default);

    Task<bool> Exists(
        GenreId id,
        CancellationToken cancellationToken = default);

    Task<bool> Exists(
        IEnumerable<GenreId> ids,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Genre genre,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Genre genre,
        CancellationToken cancellationToken = default);
}
