using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Catalog.Application.Features.Moods.Queries.GetDetails;

public sealed record GetMoodDetailsQuery(
    Guid MoodId)
    : IQuery<MoodDetails>;
