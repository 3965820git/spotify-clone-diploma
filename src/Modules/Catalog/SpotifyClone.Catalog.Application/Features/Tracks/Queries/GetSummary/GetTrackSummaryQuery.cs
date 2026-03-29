using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries.GetSummary;

public sealed record GetTrackSummaryQuery(
    Guid TrackId)
    : IQuery<TrackSummary>;
