using SpotifyClone.Playlists.Application.Features.Playlists.Queries;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Application.Abstractions.Data;

public interface IPlaylistReadService
{
    Task<PlaylistDetails?> GetDetailsAsync(
        PlaylistId id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<PlaylistSummary>> GetAllByOwnerAsync(
        UserId ownerId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<PlaylistSummary>> GetAllPublicByOwnerAsync(
        UserId ownerId,
        CancellationToken cancellationToken = default);
}
