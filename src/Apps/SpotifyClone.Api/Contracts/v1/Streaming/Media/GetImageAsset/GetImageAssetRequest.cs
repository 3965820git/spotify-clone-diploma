namespace SpotifyClone.Api.Contracts.v1.Streaming.Media.GetImageAsset;

public sealed record GetImageAssetRequest
{
    public required Guid ImageId { get; init; }
}
