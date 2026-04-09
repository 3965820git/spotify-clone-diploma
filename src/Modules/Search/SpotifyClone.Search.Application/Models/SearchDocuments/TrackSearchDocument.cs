namespace SpotifyClone.Search.Application.Models.SearchDocuments;

public sealed record TrackSearchDocument(
    string Id,
    string Title,
    string AlbumTitle,
    string[] ArtistNames,
    string[] Genres,
    string[] Moods,
    bool ContainsExplicitContent,
    string ReleaseDate,
    string CoverImageUrl);
