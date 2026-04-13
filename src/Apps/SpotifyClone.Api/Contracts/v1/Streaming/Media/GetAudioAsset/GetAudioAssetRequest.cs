namespace SpotifyClone.Api.Contracts.v1.Streaming.Media.GetAudioAsset;

public sealed record GetAudioAssetRequest
{
    public required Guid AudioId { get; init; }
}
