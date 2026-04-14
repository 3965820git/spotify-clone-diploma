using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Catalog.Application.Features.Albums.Queries.GetDetails;

public sealed record GetAlbumDetailsQuery(
    Guid AlbumId)
    : IQuery<AlbumDetails>;
