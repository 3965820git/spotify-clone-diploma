using System.Text.RegularExpressions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Streaming.Domain.Exceptions;

namespace SpotifyClone.Streaming.Domain.ValueObjects;

public sealed record PlaybackContext
    : ValueObject
{
    public const string AlbumType = "album";
    public const string PlaylistType = "playlist";
    public const string SearchType = "search";
    public const string CollectionType = "collection";

    public static readonly IEnumerable<string> Types =
        [ AlbumType, PlaylistType, SearchType, CollectionType ];

    public string Type { get; init; } = null!;
    public Guid? ExternalId { get; init; }

    public static PlaybackContext Album(Guid id) => new(AlbumType, id);
    public static PlaybackContext Playlist(Guid id) => new(PlaylistType, id);
    public static PlaybackContext Search() => new(SearchType, null);
    public static PlaybackContext Collection() => new(CollectionType, null);

    public static PlaybackContext From(string type, Guid? externalId)
    {
        string normalizedType = Regex.Replace(type.Trim().ToLowerInvariant(), @"[^0-9A-Za-z]", string.Empty);

        if ((normalizedType == AlbumType || normalizedType == PlaylistType) && externalId is null)
        {
            throw new InvalidPlaybackContextDomainException(
                $"{type} context must have an external ID.");
        }

        return normalizedType switch
        {
            AlbumType => Album(externalId!.Value),
            PlaylistType => Playlist(externalId!.Value),
            SearchType => Search(),
            CollectionType => Collection(),
            _ => throw new InvalidPlaybackContextDomainException($"Invalid context type {type}.")
        };
    }

    public static bool IsValid(string type, Guid? externalId)
    {
        string normalizedType = Regex.Replace(type.Trim().ToLowerInvariant(), @"[^0-9A-Za-z]", string.Empty);

        if ((normalizedType == AlbumType || normalizedType == PlaylistType) && externalId is null)
        {
            return false;
        }

        return Types.Contains(normalizedType);
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
