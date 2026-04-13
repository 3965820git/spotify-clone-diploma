namespace SpotifyClone.Streaming.Application.Abstractions.Services.Models;

public sealed record AudioMetadata(
    TimeSpan Duration,
    string Format,
    long SizeInBytes, // Bitrate * Duration
    long Bitrate);
