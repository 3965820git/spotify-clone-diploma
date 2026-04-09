namespace SpotifyClone.Search.Application.Models;

public sealed record AlbumSearchDocument(
    string Id,
    string Name,
    string[] ArtistNames,
    string ReleaseYear,
    string Type,
    string CoverImageUrl);
