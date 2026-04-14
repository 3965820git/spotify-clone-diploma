using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Queries.GetDetails;

public sealed record GetPlaylistDetailsQuery(
    Guid PlaylistId)
    : IQuery<PlaylistDetails>;
