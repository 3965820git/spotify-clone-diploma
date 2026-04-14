using System.Text.RegularExpressions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Catalog.Domain.Aggregates.Albums.Enums;

public sealed record AlbumType : ValueObject
{
    private const ushort MaxTracksForSingle = 3;
    private const ushort MaxTracksForExtendedPlay = 6;
    private const ushort MaxTracksForLongPlay = 50;

    public static readonly AlbumType Empty = new("empty");
    public static readonly AlbumType Single = new("single");
    public static readonly AlbumType ExtendedPlay = new("extended_play");
    public static readonly AlbumType LongPlay = new("long_play");

    public string Value { get; }

    private AlbumType(string value)
        => Value = value;

    public static AlbumType From(int trackCount)
    {
        if (trackCount == 0)
        {
            return Empty;
        }
        else if (trackCount <= MaxTracksForSingle)
        {
            return Single;
        }
        else if (trackCount <= MaxTracksForExtendedPlay)
        {
            return ExtendedPlay;
        }
        else if (trackCount <= MaxTracksForLongPlay)
        {
            return LongPlay;
        }
        else
        {
            throw new InvalidAlbumTypeDomainException(
                $"Track count must be between 0 and {MaxTracksForLongPlay}.");
        }
    }

    public static AlbumType From(string value)
        => Regex.Replace(value.Trim().ToLowerInvariant(), @"[^0-9A-Za-z]", string.Empty) switch
    {
        "single" => Single,
        "empty" => Empty,
        "extendedplay" => ExtendedPlay,
        "longplay" => LongPlay,
        _ => throw new InvalidAlbumTypeDomainException($"Invalid album type: {value}.")
    };
}
