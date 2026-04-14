using System.Text.RegularExpressions;

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Entities.Enums;

public sealed record TrackReferenceStatus
{
    public static readonly TrackReferenceStatus NotPublished = new("not_published");
    public static readonly TrackReferenceStatus Published = new("published");
    public static readonly TrackReferenceStatus Archived = new("archived");

    public bool IsPublished => this == Published;
    public bool IsArchived => this == Archived;

    public string Value { get; }

    private TrackReferenceStatus(string value)
        => Value = value;

    public static TrackReferenceStatus From(string value)
        => Regex.Replace(value.Trim().ToLowerInvariant(), @"[^0-9A-Za-z]", string.Empty) switch
        {
            "notpublished" => NotPublished,
            "published" => Published,
            "archived" => Archived,
            _ => throw new ArgumentException($"Invalid track status value: {value}")
        };
}
