using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.CorrectTitle;

public sealed record CorrectTrackTitleCommand(
    Guid TrackId,
    string Title)
    : ICatalogPersistentCommand<CorrectTrackTitleCommandResult>;
