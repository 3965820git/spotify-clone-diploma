using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.MarkAsReadyToPublish;

public sealed record MarkTrackAsReadyToPublishCommand(
    Guid TrackId)
    : ICatalogPersistentCommand<MarkTrackAsReadyToPublishCommandResult>;
