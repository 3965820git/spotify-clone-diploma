namespace SpotifyClone.Api.Contracts.v1.Streaming.Media.UploadAudioAsset;

public sealed record UploadAudioAssetRequest
{
    public required Guid TrackId { get; init; }
    public required IFormFile File { get; init; }
}
