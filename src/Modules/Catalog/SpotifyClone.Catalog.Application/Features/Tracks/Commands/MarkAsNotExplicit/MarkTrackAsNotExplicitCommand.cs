using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.MarkAsNotExplicit;

public sealed record MarkTrackAsNotExplicitCommand(
    Guid TrackId)
    : ICatalogPersistentCommand<MarkTrackAsNotExplicitCommandResult>;
