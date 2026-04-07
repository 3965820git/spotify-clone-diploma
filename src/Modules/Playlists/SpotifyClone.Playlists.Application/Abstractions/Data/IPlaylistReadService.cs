using SpotifyClone.Playlists.Application.Features.Playlists.Queries;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Pagination;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Application.Abstractions.Data;

public interface IPlaylistReadService
{
    Task<PlaylistDetails?> GetDetailsAsync(
        PlaylistId id,
        CancellationToken cancellationToken = default);

    Task<PagedList<PlaylistSummary>> GetAllAsync(
        UserId? ownerId,
        bool isAdmin,
        PaginationParams pagination,
        CancellationToken cancellationToken);
}
