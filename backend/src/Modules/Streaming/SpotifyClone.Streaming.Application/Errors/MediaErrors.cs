using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Streaming.Application.Errors;

public static class MediaErrors
{
    public static readonly Error SourceFileNotFound = new(
        "Media.SourceFileNotFound",
        "Source file was not found.");

    public static readonly Error MediaStreamNotFound = new(
        "Media.MediaStreamNotFound",
        "No media stream found in file.");

    public static readonly Error MediaAssetNotFound = CommonErrors.NotFound(
        "Media",
        "Media asset");

    public static readonly Error ConversionFailed = new(
        "Media.ConversionFailed",
        "Audio file conversion failed.");

    public static readonly Error AudioUploadFailed = new(
        "Media.AudioUploadFailed",
        "Audio file upload failed.");

    public static readonly Error ImageUploadFailed = new(
        "Media.ImageUploadFailed",
        "Image file upload failed.");

    public static readonly Error InvalidFileSize = new(
        "Media.InvalidFileSize",
        "The size of the media file is invalid.");

    public static readonly Error InvalidFormat = new(
        "Media.InvalidMediaFormat",
        "The format of the media file is invalid.");

    public static readonly Error InvalidDuration = new(
        "Media.InvalidAudioDuration",
        "The duration of the audio file is invalid.");

    public static readonly Error InvalidStatus = new(
        "Media.InvalidStatus",
        "The status of the audio file is invalid.");

    public static readonly Error InvalidImageMetadata = new(
        "Media.InvalidImageMetadata",
        "Image metadata is invalid.");

    public static readonly Error TrackNotLinkedToAudio = new(
        "Media.TrackNotLinkedToAudio",
        "A Track must be linked to Audio Asset to perform this operation.");
}
