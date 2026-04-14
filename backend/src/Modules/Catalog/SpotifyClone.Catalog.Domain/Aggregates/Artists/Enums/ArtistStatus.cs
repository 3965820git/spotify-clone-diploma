using System.Text.RegularExpressions;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Catalog.Domain.Aggregates.Artists.Enums;

public sealed record ArtistStatus : ValueObject
{
    public static readonly ArtistStatus NotVerified = new("not_verified");
    public static readonly ArtistStatus Verified = new("verified");
    public static readonly ArtistStatus Banned = new("banned");

    public bool IsVerified => this == Verified;
    public bool IsBanned => this == Banned;

    public string Value { get; }

    private ArtistStatus(string value)
        => Value = value;

    public static ArtistStatus From(string value)
        => Regex.Replace(value.Trim().ToLowerInvariant(), @"[^0-9A-Za-z]", string.Empty) switch
        {
            "notverified" => NotVerified,
            "verified" => Verified,
            "banned" => Banned,
            _ => throw new InvalidArtistStatusDomainException($"Invalid artist status value: {value}")
        };
}
