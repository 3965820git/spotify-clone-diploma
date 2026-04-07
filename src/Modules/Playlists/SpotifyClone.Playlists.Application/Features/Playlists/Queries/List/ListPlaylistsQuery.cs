using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Pagination;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Queries.List;

public sealed record ListPlaylistsQuery(
    PaginationParams Pagination)
    : IQuery<PlaylistList>;
