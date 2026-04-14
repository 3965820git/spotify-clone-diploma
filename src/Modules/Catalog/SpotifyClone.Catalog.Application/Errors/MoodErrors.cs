using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Catalog.Application.Errors;

public static class MoodErrors
{
    public static readonly Error InvalidCoverImage = new(
        "Mood.InvalidCoverImage",
        "The specified cover image is invalid.");

    public static readonly Error InvalidName = new(
        "Mood.InvalidName",
        "The specified name is invalid.");

    public static readonly Error NotFound = CommonErrors.NotFound(
        "Mood", "Mood");
}
