using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Streaming.Application.Features.Media.Queries.GetImageAsset;

public sealed record GetImageAssetQuery(
    Guid ImageId)
    : IQuery<GetImageAssetQueryResult>;
