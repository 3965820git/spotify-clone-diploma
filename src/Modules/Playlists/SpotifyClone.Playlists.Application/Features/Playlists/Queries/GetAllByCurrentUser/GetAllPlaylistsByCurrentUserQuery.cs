using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Queries.GetAllByCurrentUser;

public sealed record GetAllPlaylistsByCurrentUserQuery
    : IQuery<PlaylistList>;
