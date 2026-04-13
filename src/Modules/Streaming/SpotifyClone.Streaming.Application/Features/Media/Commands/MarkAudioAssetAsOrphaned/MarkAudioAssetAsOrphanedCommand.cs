using SpotifyClone.Streaming.Application.Abstractions;

namespace SpotifyClone.Streaming.Application.Features.Media.Commands.MarkAudioAssetAsOrphaned;

public sealed record MarkAudioAssetAsOrphanedCommand(
    Guid AudioAssetId)
    : IStreamingPersistentCommand<MarkAudioAssetAsOrphanedCommandResult>;
