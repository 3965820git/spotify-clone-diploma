using SpotifyClone.Streaming.Application.Abstractions;

namespace SpotifyClone.Streaming.Application.Features.Media.Commands.UploadAudioAsset;

public sealed record UploadAudioAssetCommand(
    string FileName,
    Stream FileStream,
    Guid TrackId)
    : IStreamingPersistentCommand<UploadAudioAssetCommandResult>;
