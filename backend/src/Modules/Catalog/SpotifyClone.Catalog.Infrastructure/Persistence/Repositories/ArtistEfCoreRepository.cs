using Microsoft.EntityFrameworkCore;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.Enums;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Catalog.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Repositories;

internal sealed class ArtistEfCoreRepository(CatalogAppDbContext context)
    : IArtistRepository
{
    private readonly DbSet<Artist> _artists = context.Artists;

    public async Task AddAsync(
        Artist artist,
        CancellationToken cancellationToken = default)
        => await _artists.AddAsync(artist, cancellationToken);

    public async Task<Artist?> GetByIdAsync(
        ArtistId id,
        CancellationToken cancellationToken = default)
        => await _artists.FirstOrDefaultAsync(
            a => a.Id == id && a.Status != ArtistStatus.Banned,
            cancellationToken);

    public async Task<IEnumerable<Artist>> GetAllByIdsAsync(
        IEnumerable<ArtistId> ids,
        CancellationToken cancellationToken = default)
        => await _artists
            .Where(a => ids.Contains(a.Id) && a.Status != ArtistStatus.Banned)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Artist>> GetAllByOwnerAsync(
        UserId ownerId,
        CancellationToken cancellationToken = default)
        => await _artists
            .Where(a => a.OwnerId == ownerId && a.Status != ArtistStatus.Banned)
            .ToListAsync(cancellationToken);

    public async Task<Artist?> GetBannedByIdAsync(
        ArtistId id,
        CancellationToken cancellationToken = default)
        => await _artists.FirstOrDefaultAsync(
            a => a.Id == id && a.Status == ArtistStatus.Banned,
            cancellationToken);

    public async Task<bool> Exists(
        ArtistId id,
        CancellationToken cancellationToken = default)
        => await _artists.AnyAsync(a => a.Id == id, cancellationToken);

    public async Task<bool> Exists(
        ISet<ArtistId> ids,
        CancellationToken cancellationToken = default)
        => await _artists.CountAsync(a => ids.Contains(a.Id), cancellationToken) == ids.Count;

    public async Task DeleteAsync(
        Artist artist,
        CancellationToken cancellationToken = default)
        => _artists.Remove(artist);
}
