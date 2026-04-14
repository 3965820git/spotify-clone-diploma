using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Streaming.Application.Abstractions.Services.Models;

namespace SpotifyClone.Streaming.Application.Abstractions.Services;

public interface IMediaService
{
    Task<AudioMetadata> GetAudioMetadataAsync(string filePath);
    Task<ImageMetadata> GetImageMetadataAsync(string filePath);

    Task<Result> ConvertToHlsDashAsync(string sourceFilePath, Guid audioId);
    Task<Result> ConvertToWebpAsync(string sourceFilePath, Guid imageId);
}
