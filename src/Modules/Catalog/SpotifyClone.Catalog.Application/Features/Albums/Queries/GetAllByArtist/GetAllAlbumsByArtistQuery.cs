using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Catalog.Application.Features.Albums.Queries.GetAllByArtist;

public sealed record GetAllAlbumsByArtistQuery(
    Guid ArtistId)
    : IQuery<AlbumList>;
