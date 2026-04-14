namespace SpotifyClone.Api.Contracts.v1.Streaming.Media.UploadImageAsset;

public sealed record UploadImageAssetRequest
{
    public required IFormFile File { get; set; }
}
