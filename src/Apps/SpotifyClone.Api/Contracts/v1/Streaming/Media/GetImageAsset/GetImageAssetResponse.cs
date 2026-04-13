namespace SpotifyClone.Api.Contracts.v1.Streaming.Media.GetImageAsset;

public sealed record GetImageAssetResponse(
    Guid ImageId,
    string WebpUrl);
