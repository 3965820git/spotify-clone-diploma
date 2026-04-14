using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Catalog.Application.Errors;

public static class AlbumErrors
{
    public static readonly Error InvalidTitle = new(
        "Album.InvalidTitle",
        "The specified title is invalid.");

    public static readonly Error InvalidType = new(
        "Album.InvalidType",
        "The specified type is invalid.");

    public static readonly Error InvalidStatus = new(
        "Album.InvalidStatus",
        "The specified status is invalid.");

    public static readonly Error InvalidCoverImage = new(
        "Album.InvalidCoverImage",
        "The specified cover image is invalid.");

    public static readonly Error InvalidReleaseDate = new(
        "Album.InvalidReleaseDate",
        "The specified release date is invalid.");

    public static readonly Error InvalidMainArtists = new(
        "Album.InvalidMainArtists",
        "The specified main artists are invalid.");

    public static readonly Error InvalidTracks = new(
        "Album.InvalidTracks",
        "The specified track is invalid.");

    public static readonly Error InvalidTrackOrder = new(
        "Album.InvalidTrackOrder",
        "The specified track order is invalid.");

    public static readonly Error AlreadyPublished = new(
        "Album.AlreadyPublished",
        "The album has already been published.");

    public static readonly Error CannotPublish = new(
        "Album.CannotPublish",
        "The album cannot be published.");

    public static readonly Error NotPublished = new(
        "Album.NotPublished",
        "The album is not published.");

    public static readonly Error MainArtistNotFound = new(
        "Album.MainArtistNotFound",
        "Main artist was not found in the album.");

    public static readonly Error InvalidTrack = new(
        "Album.InvalidTrack",
        "Track was not found in the album.");

    public static readonly Error NotOwned = new(
        "Album.NotOwned",
        "Album is not owned by the current user.");

    public static readonly Error NotFound = CommonErrors.NotFound(
        "Album", "Album");
}
