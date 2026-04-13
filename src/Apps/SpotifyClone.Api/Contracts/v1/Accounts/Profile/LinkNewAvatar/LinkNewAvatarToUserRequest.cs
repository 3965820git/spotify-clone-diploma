namespace SpotifyClone.Api.Contracts.v1.Accounts.Profile.LinkNewAvatar;

public sealed record LinkNewAvatarToUserRequest
{
    public required Guid ImageId { get; init; }
    public required int ImageWidth { get; init; }
    public required int ImageHeight { get; init; }
    public required string ImageFileType { get; init; }
    public required long ImageSizeInBytes { get; init; }
}
