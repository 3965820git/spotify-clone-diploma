using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries.GetDetails;

public sealed record GetTrackDetailsQuery(
    Guid TrackId)
    : IQuery<TrackDetails>;
