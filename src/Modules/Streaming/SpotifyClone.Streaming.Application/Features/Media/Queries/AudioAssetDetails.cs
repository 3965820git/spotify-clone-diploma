namespace SpotifyClone.Streaming.Application.Features.Media.Queries;

public sealed record AudioAssetDetails(
    Guid AudioId,
    string HlsUrl,
    string DashUrl);
