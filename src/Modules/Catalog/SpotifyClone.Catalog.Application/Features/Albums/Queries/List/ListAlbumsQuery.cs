using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Pagination;

namespace SpotifyClone.Catalog.Application.Features.Albums.Queries.List;

public sealed record ListAlbumsQuery(
    PaginationParams Pagination)
    : IQuery<AlbumList>;
