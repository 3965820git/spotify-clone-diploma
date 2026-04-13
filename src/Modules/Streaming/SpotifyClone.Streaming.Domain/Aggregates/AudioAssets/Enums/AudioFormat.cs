using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Exceptions;

namespace SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Enums;

public sealed record AudioFormat : ValueObject
{
    public static readonly AudioFormat Mp3 = new("mp3");
    public static readonly AudioFormat Wav = new("wav");
    public static readonly AudioFormat Flac = new("flac");
    public static readonly AudioFormat Aac = new("aac");

    private static readonly HashSet<string> Supported =
    [
        "mp3",
        "wav",
        "flac",
        "aac",
        "m3u8"
    ];

    public string Value { get; }

    private AudioFormat(string value)
        => Value = value;

    public static AudioFormat From(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        string normalized = value.Trim().ToLowerInvariant();

        if (!Supported.Contains(normalized))
        {
            throw new InvalidAudioFormatDomainException($"Audio format {normalized} is not supported.");
        }

        return new AudioFormat(normalized);
    }
}
