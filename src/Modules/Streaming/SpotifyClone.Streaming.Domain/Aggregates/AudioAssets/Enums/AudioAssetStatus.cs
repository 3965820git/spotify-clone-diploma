using System.Text.RegularExpressions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Exceptions;

namespace SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Enums;

public sealed record AudioAssetStatus : ValueObject
{
    public static readonly AudioAssetStatus Uploading = new("uploading");
    public static readonly AudioAssetStatus Uploaded = new("uploaded");
    public static readonly AudioAssetStatus Orphaned = new("orphaned");

    public bool IsUploading => this == Uploading;
    public bool IsUploaded => this == Uploaded;
    public bool IsOrphaned => this == Orphaned;

    public string Value { get; }

    private AudioAssetStatus(string value)
        => Value = value;

    public static AudioAssetStatus From(string value)
        => Regex.Replace(value.ToLowerInvariant(), @"[^0-9A-Za-z]", string.Empty) switch
        {
            "uploading" => Uploading,
            "uploaded" => Uploaded,
            "orphaned" => Orphaned,
            _ => throw new InvalidAudioAssetStatusDomainException($"Invalid Audio Asset status value: {value}")
        };
}
