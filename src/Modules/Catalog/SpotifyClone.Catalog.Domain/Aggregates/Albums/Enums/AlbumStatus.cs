using System.Text.RegularExpressions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Catalog.Domain.Aggregates.Albums.Enums;

public sealed record AlbumStatus : ValueObject
{
    public static readonly AlbumStatus Draft = new("draft");
    public static readonly AlbumStatus ReadyToPublish = new("ready_to_publish");
    public static readonly AlbumStatus Published = new("published");

    public bool IsDraft => this == Draft;
    public bool IsReadyToPublish => this == ReadyToPublish;
    public bool IsPublished => this == Published;

    public string Value { get; }

    private AlbumStatus(string value)
        => Value = value;

    public static AlbumStatus From(string value)
        => Regex.Replace(value.Trim().ToLowerInvariant(), @"[^0-9A-Za-z]", string.Empty) switch
        {
            "draft" => Draft,
            "readytopublish" => ReadyToPublish,
            "published" => Published,
            _ => throw new InvalidAlbumStatusDomainException($"Invalid album status value: {value}")
        };
}
