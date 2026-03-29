using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence.Converters;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Enums;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries.ValueObjects;

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Configurations.Converters;

internal static class StreamingEfCoreValueConverters
{
    public static readonly StronglyTypedIdEfCoreConverter<AudioAssetId, Guid> AudioAssetIdConverter = new(
        v => AudioAssetId.From(v));

    public static readonly StronglyTypedIdEfCoreConverter<ImageId, Guid> ImageIdConverter = new(
        v => ImageId.From(v));

    public static readonly StronglyTypedIdEfCoreConverter<UserId, Guid> UserIdConverter = new(
        v => UserId.From(v));

    public static readonly StronglyTypedIdEfCoreConverter<TrackId, Guid> TrackIdConverter = new(
        v => TrackId.From(v));

    public static readonly StronglyTypedIdEfCoreConverter<PlaybackHistoryEntryId, Guid>
        PlaybackHistoryEntryIdConverter = new(v => PlaybackHistoryEntryId.From(v));

    public static readonly ValueConverter<AudioFormat?, string?> AudioFormatConverter = new(
        f => f == null ? null : f.Value,
        v => v == null ? null : AudioFormat.From(v));

    public static readonly ValueConverter<AudioAssetStatus, string> AudioAssetStatusConverter = new(
        f => f.Value,
        v => AudioAssetStatus.From(v));

    public static readonly ValueConverter<ImageFileType, string> ImageFileTypeConverter = new(
        t => t.Value,
        v => ImageFileType.From(v));

    public static readonly ValueConverter<TrackId?, Guid?> NullableTrackIdConverter = new(
        id => id == null ? null : id.Value,
        v => v == null ? null : TrackId.From((Guid)v));
}
