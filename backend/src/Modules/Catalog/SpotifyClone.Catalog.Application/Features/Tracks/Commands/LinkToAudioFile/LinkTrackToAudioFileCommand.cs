using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.LinkToAudioFile;

public sealed record LinkTrackToAudioFileCommand(
    Guid TrackId,
    Guid AudioFileId,
    TimeSpan Duration)
    : ICatalogPersistentCommand<LinkTrackToAudioFileCommandResult>;
