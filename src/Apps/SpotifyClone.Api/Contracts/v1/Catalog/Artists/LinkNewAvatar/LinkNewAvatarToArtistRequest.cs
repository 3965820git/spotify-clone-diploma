namespace SpotifyClone.Api.Contracts.v1.Catalog.Artists.LinkNewAvatar;

public sealed record LinkNewAvatarToArtistRequest
{
    public required Guid ImageId { get; init; }
    public required int ImageWidth { get; init; }
    public required int ImageHeight { get; init; }
    public required string ImageFileType { get; init; }
    public required long ImageSizeInBytes { get; init; }
}
