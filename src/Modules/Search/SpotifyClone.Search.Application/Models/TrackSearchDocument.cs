namespace SpotifyClone.Search.Application.Models;

public sealed record TrackSearchDocument(
    string Id,
    string Title,
    string[] AlbumTitles,
    string[] MainArtistNames,
    string[] FeaturedArtistNames);
