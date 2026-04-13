using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Catalog.Application.Errors;

public static class CommonCatalogErrors
{
    public static readonly Error InvalidImageMetadata = new(
        "Image.InvalidMetadata",
        "Image metadata is invalid.");
}
