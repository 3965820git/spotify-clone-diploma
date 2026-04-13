using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Playlists.Infrastructure.Persistence.Entities.Enums;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence.Converters;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Configurations.Converters;

internal static class PlaylistsEfCoreValueConverters
{
    public static readonly StronglyTypedIdEfCoreConverter<PlaylistId, Guid> PlaylistIdConverter = new(
        v => PlaylistId.From(v));

    public static readonly StronglyTypedIdEfCoreConverter<UserId, Guid> UserIdConverter = new(
        v => UserId.From(v));

    public static readonly StronglyTypedIdEfCoreConverter<ImageId, Guid> ImageIdConverter = new(
        v => ImageId.From(v));

    public static readonly StronglyTypedIdEfCoreConverter<TrackId, Guid> TrackIdConverter = new(
        v => TrackId.From(v));

    public static readonly ValueConverter<ImageFileType, string> ImageFileTypeConverter = new(
        t => t.Value,
        v => ImageFileType.From(v));

    public static readonly ValueConverter<TrackReferenceStatus, string> CatalogTrackStatusConverter = new(
        t => t.Value,
        v => TrackReferenceStatus.From(v));
}
