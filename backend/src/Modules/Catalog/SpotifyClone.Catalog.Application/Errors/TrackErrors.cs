using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Catalog.Application.Errors;

public static class TrackErrors
{
    public static readonly Error InvalidTitle = new(
        "Track.InvalidTitle",
        "The specified title is invalid.");

    public static readonly Error InvalidDuration = new(
        "Track.InvalidDuration",
        "The specified duration is invalid.");

    public static readonly Error InvalidMainArtists = new(
        "Track.InvalidMainArtists",
        "The specified main artists are invalid.");

    public static readonly Error InvalidGenres = new(
        "Track.InvalidGenres",
        "The specified genres are invalid.");

    public static readonly Error InvalidMoods = new(
        "Track.InvalidMoods",
        "The specified moods are invalid.");

    public static readonly Error InvalidReleaseDate = new(
        "Track.InvalidReleaseDate",
        "The specified release date is invalid.");

    public static readonly Error InvalidTrackStatus = new(
        "Track.InvalidTrackStatus",
        "Track status is invalid.");

    public static readonly Error CannotMarkAsReadyToPublish = new(
        "Track.CannotMarkAsReadyToPublish",
        "The specified track cannot be marked as ready to publish.");

    public static readonly Error CannotPublish = new(
        "Track.CannotPublish",
        "The specified track cannot be published.");

    public static readonly Error AlreadyPublished = new(
        "Track.AlreadyPublished",
        "The track has already been published.");

    public static readonly Error NotPublished = new(
        "Track.NotPublished",
        "The specified track is not published.");

    public static readonly Error AlreadyLinkedToAudioFile = new(
        "Track.AlreadyLinkedToAudio",
        "Track is already linked to an audio file.");

    public static readonly Error AlreadyReleased = new(
        "Track.AlreadyReleased",
        "The specified track is already released.");

    public static readonly Error AudioFileInUse = new(
        "Track.AudioFileInUse",
        "The specified audio file is already in use.");

    public static readonly Error AlreadyAttachedToAnAlbum = new(
        "Track.AlreadyAttachedToAnAlbum",
        "The specified track is already attached to an album.");

    public static readonly Error NotOwned = new(
        "Track.NotOwned",
        "Track is not owned by the current user.");

    public static readonly Error NotFound = CommonErrors.NotFound(
        "Track", "Track");
}
