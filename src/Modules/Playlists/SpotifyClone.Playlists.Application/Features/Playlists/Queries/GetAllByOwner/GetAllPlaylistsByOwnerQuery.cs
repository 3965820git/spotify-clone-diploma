using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Queries.GetAllByOwner;

public sealed record GetAllPlaylistsByOwnerQuery(
    Guid OwnerId)
    : IQuery<PlaylistList>;
