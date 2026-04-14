namespace SpotifyClone.Playlists.Application.Models;

public sealed record ImageMetadataDetails(
    Guid ImageId,
    int Width,
    int Height,
    string FileType,
    long SizeInBytes);
