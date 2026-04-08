namespace SpotifyClone.Search.Application.Models;

public sealed record AlbumSearchDocument(
    string Id,
    string Name,
    string[] MainArtistNames,
    string[] FeaturedArtistNames);
