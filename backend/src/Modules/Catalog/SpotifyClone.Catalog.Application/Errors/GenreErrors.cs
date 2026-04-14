using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Catalog.Application.Errors;

public static class GenreErrors
{
    public static readonly Error InvalidCoverImage = new(
        "Genre.InvalidCoverImage",
        "The specified cover image is invalid.");

    public static readonly Error InvalidName = new(
        "Genre.InvalidName",
        "The specified name is invalid.");

    public static readonly Error NotFound = CommonErrors.NotFound(
        "Genre", "Genre");
}
