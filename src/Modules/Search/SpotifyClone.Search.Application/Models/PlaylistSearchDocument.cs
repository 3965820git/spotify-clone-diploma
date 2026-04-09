namespace SpotifyClone.Search.Application.Models;

public sealed record PlaylistSearchDocument(
    string Id,
    string Name,
    string OwnerDisplayName,
    int TracksCount,
    string CoverImageUrl,
    string[] GeneratedCoverImageIds);
