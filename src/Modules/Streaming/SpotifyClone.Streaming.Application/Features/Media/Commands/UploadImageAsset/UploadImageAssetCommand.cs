using SpotifyClone.Streaming.Application.Abstractions;

namespace SpotifyClone.Streaming.Application.Features.Media.Commands.UploadImageAsset;

public sealed record UploadImageAssetCommand(
    string FileName,
    Stream FileStream)
    : IStreamingPersistentCommand<UploadImageAssetCommandResult>;
