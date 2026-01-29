using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Exceptions;

namespace SpotifyClone.Shared.Kernel.Enums;

public sealed record ImageFileType : ValueObject
{
    public readonly static ImageFileType Jpg = new("jpg");
    public readonly static ImageFileType Jpeg = new("jpeg");
    public readonly static ImageFileType Png = new("png");
    public readonly static ImageFileType Webp = new("webp");

    private static readonly HashSet<string> Supported =
    [
        "jpg",
        "jpeg",
        "png",
        "webp"
    ];

    public string Value { get; }

    private ImageFileType(string value)
        => Value = value;

    public static ImageFileType From(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        string normalized = value.Trim().ToLowerInvariant();

        if (!Supported.Contains(normalized))
        {
            throw new InvalidImageFileTypeDomainException($"Image file type '{normalized}' is not supported.");
        }

        return new ImageFileType(normalized);
    }

    public bool SupportsTransparency =>
        Value is "png" or "webp";
}
