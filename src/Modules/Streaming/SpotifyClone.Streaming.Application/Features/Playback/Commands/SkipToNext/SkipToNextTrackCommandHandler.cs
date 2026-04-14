using MediatR;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
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
    ICurrentUser currentUser,
    IPublisher publisher)
    : ICommandHandler<SkipToNextTrackCommand, SkipToNextTrackCommandResult>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly IAudioAssetReadService _audioAssetReadService = audioAssetReadService;
    private readonly IFileStorage _storage = storage;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IPublisher _publisher = publisher;

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
            return Result.Failure<SkipToNextTrackCommandResult>(PlaybackErrors.SessionNotFound);
        }

        session.SkipToNext(DeviceId.From(request.DeviceId));

        if (session.TrackId is null)
        {
            return new SkipToNextTrackCommandResult(session.Queue.IsCurrentTracksEmpty);
        }

        AudioAssetId? id = await _audioAssetReadService.GetByTrackId(session.TrackId, cancellationToken);
        if (id is null)
        {
            return Result.Failure<SkipToNextTrackCommandResult>(MediaErrors.MediaAssetNotFound);
        }

        string baseUrl = _storage.GetAudioRootPath();
        string hlsUrl;
        string? dashUrl;
        if (_currentUser.IsPremium)
        {
            hlsUrl = $"{baseUrl}/{id.Value}/master.m3u8";
            dashUrl = $"{baseUrl}/{id.Value}/manifest.mpd";
        }
        else
        {
            hlsUrl = $"{baseUrl}/{id.Value}/media_0.m3u8";
            dashUrl = null;
        }

        // needs to be moved into _unit.CommitAsync() somehow
        await _unit.PlaybackSessions.SaveAsync(session, cancellationToken);
        foreach (DomainEvent domainEvent in session.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return new SkipToNextTrackCommandResult(
            session.Queue.IsCurrentTracksEmpty,
            hlsUrl, dashUrl,
            session.CurrentPositionMs,
            session.TrackId.Value);
    }
}
