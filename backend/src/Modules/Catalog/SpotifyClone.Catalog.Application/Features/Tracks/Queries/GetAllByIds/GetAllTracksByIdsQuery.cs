using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries.GetAllByIds;

public sealed record GetAllTracksByIdsQuery(
    IEnumerable<Guid> TrackIds)
    : IQuery<TrackList>;
