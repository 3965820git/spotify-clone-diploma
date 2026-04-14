using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Enums;

namespace SpotifyClone.Shared.Kernel.ValueObjects;

public sealed record ImageMetadata(
    int Width,
    int Height,
    ImageFileType FileType,
    long SizeInBytes)
    : ValueObject;
