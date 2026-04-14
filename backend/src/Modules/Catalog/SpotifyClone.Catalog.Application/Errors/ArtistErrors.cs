using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Catalog.Application.Errors;

public static class ArtistErrors
{
    public static readonly Error InvalidAvatarImage = new(
        "Artist.InvalidAvatarImage",
        "The specified avatar image is invalid.");

    public static readonly Error InvalidBannerImage = new(
        "Artist.InvalidBannerImage",
        "The specified banner image is invalid.");

    public static readonly Error InvalidGalleryImage = new(
        "Artist.InvalidGalleryImage",
        "The specified gallery image is invalid.");

    public static readonly Error InvalidName = new(
        "Artist.InvalidName",
        "The specified name is invalid.");

    public static readonly Error InvalidBio = new(
        "Artist.InvalidBio",
        "The specified bio is invalid.");

    public static readonly Error NotVerified = new(
        "Artist.NotVerified",
        "The artist has not been verified.");

    public static readonly Error CannotVerify = new(
        "Artist.CannotVerify",
        "Artist cannot be verified.");

    public static readonly Error Banned = new(
        "Artist.Banned",
        "The specified artist is banned.");

    public static readonly Error InvalidStatus = new(
        "Artist.InvalidStatus",
        "The specified artist status is invalid.");

    public static readonly Error NotOwned = new(
        "Artist.NotOwned",
        "Artist is not owned by the current user.");

    public static readonly Error NotFound = CommonErrors.NotFound(
        "Artist", "Artist");
}
