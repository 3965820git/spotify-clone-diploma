using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Streaming.Application.Features.Media.Queries.GetAudioAsset;

public sealed record GetAudioAssetQuery(
    Guid AudioId)
    : IQuery<AudioAssetDetails>;
