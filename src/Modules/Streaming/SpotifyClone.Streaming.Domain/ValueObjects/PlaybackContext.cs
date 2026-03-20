using System.Text.RegularExpressions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Streaming.Domain.Exceptions;

namespace SpotifyClone.Streaming.Domain.ValueObjects;

public sealed record PlaybackContext
    : ValueObject
{
    public string Type { get; init; } = null!;
    public Guid? ExternalId { get; init; }

    public static PlaybackContext Album(Guid id) => new("album", id);
    public static PlaybackContext Playlist(Guid id) => new("playlist", id);
    public static PlaybackContext Search() => new("search", null);
    public static PlaybackContext Collection() => new("collection", null);

    public static PlaybackContext From(string type, Guid? externalId)
    {
        string normalizedType = Regex.Replace(type.Trim().ToLowerInvariant(), @"[^0-9A-Za-z]", string.Empty);

        if ((normalizedType == "album" || normalizedType == "playlist") && externalId is null)
        {
            throw new InvalidPlaybackContextDomainException(
                $"{type} context must have an external ID.");
        }

        return normalizedType switch
        {
            "album" => Album(externalId!.Value),
            "playlist" => Playlist(externalId!.Value),
            "search" => Search(),
            "collection" => Collection(),
            _ => throw new InvalidPlaybackContextDomainException($"Invalid context type {type}.")
        };
    }

    private PlaybackContext(string type, Guid? externalId)
    {
        Type = type;
        ExternalId = externalId;
    }

    private PlaybackContext()
    {
    }
}
