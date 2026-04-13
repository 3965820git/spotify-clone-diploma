namespace SpotifyClone.Api.Contracts.v1.Streaming.Media.GetAudioAsset;

public sealed record GetAudioAssetResponse(
    Guid AudioId,
    string HlsUrl,
    string DashUrl);
