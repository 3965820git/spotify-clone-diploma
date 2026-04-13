namespace SpotifyClone.Streaming.Infrastructure.Storage;

public sealed record MinioOptions
{
    public const string SectionName = "Minio";
    public required string Endpoint { get; init; }
    public required int Port { get; init; }
    public required string AccessKey { get; init; }
    public required string SecretKey { get; init; }
    public required string StorageUrl { get; init; }
    public required string LocalScratchPath { get; init; }
    public required string AudioBucketName { get; init; }
    public required string ImageBucketName { get; init; }
}
