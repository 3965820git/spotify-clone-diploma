using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Abstractions.Data;
using SpotifyClone.Streaming.Application.Abstractions.Services;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;
using SpotifyClone.Streaming.Domain.ValueObjects;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SkipToNext;

internal sealed class SkipToNextTrackCommandHandler(
    IStreamingUnitOfWork unit,
    IAudioAssetReadService audioAssetReadService,
    IFileStorage storage,
    ICurrentUser currentUser)
    : ICommandHandler<SkipToNextTrackCommand, SkipToNextTrackCommandResult>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly IAudioAssetReadService _audioAssetReadService = audioAssetReadService;
    private readonly IFileStorage _storage = storage;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<SkipToNextTrackCommandResult>> Handle(
        SkipToNextTrackCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Result.Failure<SkipToNextTrackCommandResult>(PlaybackErrors.NotLoggedIn);
        }

        PlaybackSession? session = await _unit.PlaybackSessions.GetByUserIdAsync(
            UserId.From(_currentUser.Id), cancellationToken);
        if (session is null)
        {
            return Result.Failure<SkipToNextTrackCommandResult>(PlaybackErrors.NotFound);
        }

        session.SkipToNext(DeviceId.From(request.DeviceId));
        await _unit.PlaybackSessions.SaveAsync(session, cancellationToken);

        if (session.TrackId is null)
        {
            return new SkipToNextTrackCommandResult(session.Queue.IsEmpty);
        }

        AudioAssetId? id = await _audioAssetReadService.GetByTrackId(session.TrackId, cancellationToken);
        if (id is null)
        {
            return Result.Failure<SkipToNextTrackCommandResult>(MediaErrors.MediaAssetNotFound);
        }

        string baseUrl = _storage.GetAudioRootPath();
        string hlsUrl = $"{baseUrl}/{id.Value}/master.m3u8";
        string dashUrl = $"{baseUrl}/{id.Value}/manifest.mpd";

        return new SkipToNextTrackCommandResult(
            session.Queue.IsEmpty,
            hlsUrl, dashUrl,
            session.CurrentPositionMs,
            session.TrackId.Value);
    }
}
