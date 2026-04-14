using Microsoft.EntityFrameworkCore;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.Enums;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Playlists.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Repositories;

internal sealed class PlaylistEfCoreRepository(PlaylistsAppDbContext context)
    : IPlaylistRepository
{
    private readonly DbSet<Playlist> _playlists = context.Playlists;

    public async Task AddAsync(
        Playlist playlist,
        CancellationToken cancellationToken = default)
        => await _playlists.AddAsync(playlist, cancellationToken);

    public async Task<Playlist?> GetByIdAsync(
        PlaylistId id,
        CancellationToken cancellationToken = default)
        => await _playlists
            .Where(p => p.Id == id)
            .Include(p => p.Collaborators)
            .Include(p => p.Tracks)
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<Playlist?> GetLikedTracksAsync(
        UserId ownerId,
        CancellationToken cancellationToken = default)
        => await _playlists
            .Where(p => p.OwnerId == ownerId && p.Type == PlaylistType.LikedTracks)
            .Include(p => p.Collaborators)
            .Include(p => p.Tracks)
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<Playlist?> GetArchivedTracksAsync(
        UserId ownerId,
        CancellationToken cancellationToken = default)
        => await _playlists
            .Where(p => p.OwnerId == ownerId && p.Type == PlaylistType.ArchivedTracks)
            .Include(p => p.Collaborators)
            .Include(p => p.Tracks)
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<Playlist>> GetByIdsAsync(
        IEnumerable<PlaylistId> ids,
        CancellationToken cancellationToken = default)
    {
        if (!ids.Any())
        {
            return [];
        }

        return await _playlists
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Playlist>> GetAllByOwnerAsync(
        UserId ownerId,
        CancellationToken cancellationToken = default)
        => await _playlists
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync(cancellationToken);

    public async Task DeleteAsync(
        Playlist playlist,
        CancellationToken cancellationToken = default)
        => _playlists.Remove(playlist);

    public async Task DeleteAllAsync(
        IEnumerable<Playlist> playlists,
        CancellationToken cancellationToken = default)
        => _playlists.RemoveRange(playlists);
}
