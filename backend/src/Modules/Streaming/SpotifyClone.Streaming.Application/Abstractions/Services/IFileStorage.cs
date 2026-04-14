namespace SpotifyClone.Streaming.Application.Abstractions.Services;

public interface IFileStorage
{
    string GetImageRootPath();

    string GetAudioRootPath();

    string GetLocalConversionRootPath();

    Task SaveTempFileToLocal(Stream stream, string relativePath);

    Task DeleteTempDirectoryFromLocal(string relativePath);

    Task SaveImageFileAsync(Stream stream, string relativePath);

    Task SaveAudioFileAsync(Stream stream, string relativePath);

    Task DeleteAudioFileAsync(string folderPath);

    Task DeleteImageFileAsync(string folderPath);

    Task DownloadAudioToLocalFileAsync(string objectName, string localPath);

    Task DownloadImageToLocalFileAsync(string objectName, string localPath);
}
