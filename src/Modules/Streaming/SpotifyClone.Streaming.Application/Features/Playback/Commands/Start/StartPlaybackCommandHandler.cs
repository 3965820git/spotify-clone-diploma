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
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;
using SpotifyClone.Streaming.Domain.ValueObjects;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.Start;

internal sealed class StartPlaybackCommandHandler(
    IStreamingUnitOfWork unit,
    IAudioAssetReadService audioAssetReadService,
    IFileStorage storage,
    ICurrentUser currentUser)
    : ICommandHandler<StartPlaybackCommand, StartPlaybackCommandResult>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly IAudioAssetReadService _audioAssetReadService = audioAssetReadService;
    private readonly IFileStorage _storage = storage;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<StartPlaybackCommandResult>> Handle(
        StartPlaybackCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Result.Failure<StartPlaybackCommandResult>(PlaybackErrors.NotLoggedIn);
        }

        var userId = UserId.From(_currentUser.Id);
        var deviceId = DeviceId.From(request.DeviceId);
        var context = PlaybackContext.From(request.ContextType, request.ContextExternalId);
        DateTimeOffset nowUtc = DateTimeOffset.UtcNow;
        IEnumerable<TrackId> tracks = request.TrackIds.Select(t => TrackId.From(t));

        PlaybackSession? session = await _unit.PlaybackSessions.GetByUserIdAsync(userId, cancellationToken);
        if (session is null)
        {
            session = PlaybackSession.Create(
                PlaybackSessionId.New(), userId, deviceId, context, nowUtc, tracks.ToList());
        }
        else
        {
            session.StartNewPlayback(deviceId, context, nowUtc, 0, tracks);
        }
        await _unit.PlaybackSessions.SaveAsync(session, cancellationToken);

        AudioAssetId? id = await _audioAssetReadService.GetByTrackId(session.TrackId, cancellationToken);
        if (id is null)
        {
            return Result.Failure<StartPlaybackCommandResult>(MediaErrors.MediaAssetNotFound);
        }

        string baseUrl = _storage.GetAudioRootPath();
        string hlsUrl = $"{baseUrl}/{id.Value}/master.m3u8";
        string dashUrl = $"{baseUrl}/{id.Value}/manifest.mpd";

        return new StartPlaybackCommandResult(
            hlsUrl, dashUrl,
            session.CurrentPositionMs,
            session.TrackId.Value);
    }
}
