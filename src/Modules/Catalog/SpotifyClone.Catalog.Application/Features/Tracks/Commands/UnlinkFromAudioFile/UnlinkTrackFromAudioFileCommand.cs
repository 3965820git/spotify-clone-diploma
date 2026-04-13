using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UnlinkFromAudioFile;

public sealed record UnlinkTrackFromAudioFileCommand(
    Guid TrackId)
    : ICatalogPersistentCommand<UnlinkTrackFromAudioFileCommandResult>;
