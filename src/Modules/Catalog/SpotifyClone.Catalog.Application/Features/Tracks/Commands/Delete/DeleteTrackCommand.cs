using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.Delete;

public sealed record DeleteTrackCommand(
    Guid TrackId)
    : ICatalogPersistentCommand<DeleteTrackCommandResult>;
