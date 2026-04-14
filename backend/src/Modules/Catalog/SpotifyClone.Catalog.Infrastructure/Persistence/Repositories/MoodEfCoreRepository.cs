using Microsoft.EntityFrameworkCore;
using SpotifyClone.Catalog.Domain.Aggregates.Moods;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;
using SpotifyClone.Catalog.Infrastructure.Persistence.Database;

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Repositories;

internal sealed class MoodEfCoreRepository(CatalogAppDbContext context)
    : IMoodRepository
{
    private readonly DbSet<Mood> _moods = context.Moods;

    public async Task<bool> IsNameUniqueAsync(
        string name,
        CancellationToken cancellationToken = default)
        => !await _moods.AnyAsync(a => a.Name == name, cancellationToken);

    public async Task<bool> Exists(
        MoodId id,
        CancellationToken cancellationToken = default)
        => await _moods.AnyAsync(a => a.Id == id, cancellationToken);

    public async Task<bool> Exists(
        IEnumerable<MoodId> ids,
        CancellationToken cancellationToken = default)
        => await _moods.CountAsync(a => ids.Contains(a.Id), cancellationToken) == ids.Count();

    public async Task<Mood?> GetByIdAsync(
        MoodId id,
        CancellationToken cancellationToken = default)
        => await _moods.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task<IEnumerable<Mood>> GetByIdsAsync(
        IEnumerable<MoodId> ids,
        CancellationToken cancellationToken = default)
        => await _moods
            .Where(a => ids.Contains(a.Id))
            .ToListAsync(cancellationToken);

    public async Task DeleteAsync(
        Mood mood,
        CancellationToken cancellationToken = default)
        => _moods.Remove(mood);

    public async Task AddAsync(
        Mood mood,
        CancellationToken cancellationToken = default)
        => await _moods.AddAsync(mood, cancellationToken);
}
