namespace SpotifyClone.Streaming.Application.Features.Media.Queries.GetImageAsset;

public sealed record GetImageAssetQueryResult(
    Guid ImageId,
    string WebpUrl);
