using Microsoft.EntityFrameworkCore;
using SpotifyClone.Catalog.Domain.Aggregates.Genres;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Catalog.Infrastructure.Persistence.Database;

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Repositories;

internal sealed class GenreEfCoreRepository(CatalogAppDbContext context)
    : IGenreRepository
{
    private readonly DbSet<Genre> _genres = context.Genres;

    public async Task<bool> IsNameUniqueAsync(
        string name,
        CancellationToken cancellationToken = default)
        => !await _genres.AnyAsync(a => a.Name == name, cancellationToken);

    public async Task<bool> Exists(
        GenreId id,
        CancellationToken cancellationToken = default)
        => await _genres.AnyAsync(a => a.Id == id, cancellationToken);

    public async Task<bool> Exists(
        IEnumerable<GenreId> ids,
        CancellationToken cancellationToken = default)
        => await _genres.CountAsync(a => ids.Contains(a.Id), cancellationToken) == ids.Count();

    public async Task<Genre?> GetByIdAsync(
        GenreId id,
        CancellationToken cancellationToken = default)
        => await _genres.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task<IEnumerable<Genre>> GetByIdsAsync(
        IEnumerable<GenreId> ids,
        CancellationToken cancellationToken = default)
        => await _genres
            .Where(a => ids.Contains(a.Id))
            .ToListAsync(cancellationToken);

    public async Task DeleteAsync(
        Genre genre,
        CancellationToken cancellationToken = default)
        => _genres.Remove(genre);

    public async Task AddAsync(
        Genre genre,
        CancellationToken cancellationToken = default)
        => await _genres.AddAsync(genre, cancellationToken);
}
