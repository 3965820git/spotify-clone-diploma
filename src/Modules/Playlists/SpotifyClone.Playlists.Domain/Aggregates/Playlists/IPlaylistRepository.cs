using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Domain.Aggregates.Playlists;

public interface IPlaylistRepository
{
    Task<Playlist?> GetByIdAsync(
        PlaylistId id,
        CancellationToken cancellationToken = default);

    Task<Playlist?> GetLikedTracksAsync(
        UserId ownerId,
        CancellationToken cancellationToken = default);

    Task<Playlist?> GetArchivedTracksAsync(
        UserId ownerId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Playlist>> GetByIdsAsync(
        IEnumerable<PlaylistId> ids,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Playlist>> GetAllByOwnerAsync(
        UserId ownerId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Playlist playlist,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Playlist playlist,
        CancellationToken cancellationToken = default);

    Task DeleteAllAsync(
        IEnumerable<Playlist> playlists,
        CancellationToken cancellationToken = default);
}
