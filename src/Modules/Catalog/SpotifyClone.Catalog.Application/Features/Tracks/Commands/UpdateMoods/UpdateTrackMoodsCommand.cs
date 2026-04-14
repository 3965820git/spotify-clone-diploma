using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateMoods;

public sealed record UpdateTrackMoodsCommand(
    Guid TrackId,
    IEnumerable<Guid> Moods)
    : ICatalogPersistentCommand<UpdateTrackMoodsCommandResult>;
