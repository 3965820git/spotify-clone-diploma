namespace SpotifyClone.Search.Application.Models.SearchDocuments;

public sealed record PlaylistSearchDocument(
    string Id,
    string Name,
    string OwnerDisplayName,
    int TracksCount,
    string CoverImageUrl,
    string[] GeneratedCoverImageIds);
