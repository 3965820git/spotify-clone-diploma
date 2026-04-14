using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.MarkAsExplicit;

public sealed record MarkTrackAsExplicitCommand(
    Guid TrackId)
    : ICatalogPersistentCommand<MarkTrackAsExplicitCommandResult>;
