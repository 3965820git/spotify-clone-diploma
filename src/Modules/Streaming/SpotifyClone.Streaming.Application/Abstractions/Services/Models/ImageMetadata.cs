namespace SpotifyClone.Streaming.Application.Abstractions.Services.Models;

public sealed record ImageMetadata(
    int Width,
    int Height,
    string FileType,
    long SizeInBytes);
