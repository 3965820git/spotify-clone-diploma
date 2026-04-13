using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Streaming.Application.Abstractions.Services;
using SpotifyClone.Streaming.Application.Abstractions.Services.Models;
using SpotifyClone.Streaming.Application.Errors;
using Xabe.FFmpeg;

namespace SpotifyClone.Streaming.Infrastructure.Media;

public class FfmpegMediaService(
    ILogger<FfmpegMediaService> logger,
    IFileStorage storage) : IMediaService
{
    private readonly ILogger<FfmpegMediaService> _logger = logger;
    private readonly IFileStorage _storage = storage;

    public async Task<AudioMetadata> GetAudioMetadataAsync(string filePath)
    {
        // 1. Отримуємо інфо про файл з диска
        var fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
        {
            throw new FileNotFoundException("Local file not found", filePath);
        }

        // 2. Аналізуємо через FFmpeg
        IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(filePath);
        IAudioStream? audioStream = mediaInfo.AudioStreams.FirstOrDefault();

        return new AudioMetadata(
            audioStream?.Duration ?? TimeSpan.Zero,
            Path.GetExtension(filePath).TrimStart('.'),
            fileInfo.Length, // Локальний розмір
            audioStream?.Bitrate ?? 0
        );
    }

    public async Task<ImageMetadata> GetImageMetadataAsync(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
        {
            throw new FileNotFoundException("Local file not found", filePath);
        }

        IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(filePath);
        IVideoStream? videoStream = mediaInfo.VideoStreams.FirstOrDefault();

        return new ImageMetadata(
            videoStream?.Width ?? 0,
            videoStream?.Height ?? 0,
            Path.GetExtension(filePath).TrimStart('.'),
            fileInfo.Length
        );
    }

    public async Task<Result> ConvertToHlsDashAsync(string sourceFilePath, Guid audioId)
    {
        if (!File.Exists(sourceFilePath))
        {
            return Result.Failure(MediaErrors.SourceFileNotFound);
        }

        string specificAudioFolder = Path.Combine(_storage.GetLocalConversionRootPath(), audioId.ToString());

        try
        {
            // 1. Створюємо головну папку
            if (!Directory.Exists(specificAudioFolder))
            {
                Directory.CreateDirectory(specificAudioFolder);
            }

            // 2. Створюємо папки 0 та 1 вручну (FFmpeg буде використовувати їх як Representation ID)
            string[] representationIds = ["0", "1"];
            foreach (string id in representationIds)
            {
                string subFolder = Path.Combine(specificAudioFolder, id);
                if (!Directory.Exists(subFolder))
                {
                    Directory.CreateDirectory(subFolder);
                }
            }

            IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(sourceFilePath);
            IAudioStream? audioStream = mediaInfo.AudioStreams.FirstOrDefault();

            if (audioStream == null)
            {
                return Result.Failure(MediaErrors.MediaStreamNotFound);
            }

            _logger.LogInformation("Starting CMAF (HLS + DASH) conversion for {AudioId}...", audioId);

            string normalizedPath = specificAudioFolder.Replace("\\", "/");

            IConversion conversion = FFmpeg.Conversions.New()
                .AddStream(audioStream)
                .AddParameter("-map 0:a:0")

                .AddParameter("-c:a:0 aac -b:a:0 128k")
                .AddParameter("-c:a:1 aac -b:a:1 192k")
                .AddParameter("-vn")

                .AddParameter("-f dash")
                .AddParameter("-hls_playlist 1")

                .AddParameter("-seg_duration 10")
                .AddParameter("-use_template 1")
                .AddParameter("-use_timeline 1")
                .AddParameter("-dash_segment_type mp4")

                // Шаблон для ініт-файлів та медіа-чанків всередині папок
                .AddParameter($"-init_seg_name \"$RepresentationID$/init.mp4\"")
                .AddParameter($"-media_seg_name \"$RepresentationID$/chunk_$Number%03d$.m4s\"")

                // %v підставить 0 або 1 автоматично
                .AddParameter($"-hls_segment_filename \"{normalizedPath}/%v/chunk_%03d.m4s\"")

                .SetOutput($"{normalizedPath}/manifest.mpd");

            await conversion.Start();

            _logger.LogInformation("Multi-Bitrate HLS/DASH conversion finished for {AudioId}", audioId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while converting song. Cleaning up...");

            if (Directory.Exists(specificAudioFolder))
            {
                Directory.Delete(specificAudioFolder, true);
            }

            return Result.Failure(MediaErrors.ConversionFailed);
        }
    }

    public async Task<Result> ConvertToWebpAsync(string sourceFilePath, Guid imageId)
    {
        if (!File.Exists(sourceFilePath))
        {
            return Result.Failure(MediaErrors.SourceFileNotFound);
        }

        string specificImageFolder = Path.Combine(_storage.GetLocalConversionRootPath(), imageId.ToString());
        string outputFilePath = Path.Combine(specificImageFolder, "image.webp");

        try
        {
            // 1. Створюємо папку
            if (!Directory.Exists(specificImageFolder))
            {
                Directory.CreateDirectory(specificImageFolder);
            }

            // 2. Отримуємо інфо про файл (FFmpeg бачить картинки як VideoStream)
            IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(sourceFilePath);
            IVideoStream? videoStream = mediaInfo.VideoStreams.FirstOrDefault();
            if (videoStream == null)
            {
                return Result.Failure(MediaErrors.MediaStreamNotFound);
            }

            _logger.LogInformation("Starting WebP conversion for {ImageId}...", imageId);

            // 3. Налаштовуємо конвертацію
            IConversion conversion = FFmpeg.Conversions.New()
                .AddStream(videoStream)
                .SetOutput(outputFilePath)

                // Вказуємо кодек WebP
                .AddParameter("-c:v libwebp")

                // Якість (Compression Factor): від 0 до 100.
                // 75-80 — це "золотий стандарт" Google (баланс між розміром і якістю).
                // 100 — максимальна якість (але не lossless).
                .AddParameter("-q:v 80")

                // Прибираємо аудіо (на випадок, якщо хтось завантажив відео замість картинки)
                .AddParameter("-an")

                // Оптимізація: Preset 'picture' підказує кодеку, що це фото, а не графіка/текст
                .AddParameter("-preset picture")

                // Loop 0 потрібен, якщо раптом завантажать GIF, щоб він не крутився вічно (для статики)
                // або навпаки, якщо хочеш підтримку анімованих WebP, прибери це.
                .AddParameter("-frames:v 1");

            await conversion.Start();

            _logger.LogInformation("WebP conversion finished for {ImageId}", imageId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error converting image {ImageId}. Cleaning up...", imageId);

            if (Directory.Exists(specificImageFolder))
            {
                Directory.Delete(specificImageFolder, true);
            }

            return Result.Failure(MediaErrors.ConversionFailed);
        }
    }
}
