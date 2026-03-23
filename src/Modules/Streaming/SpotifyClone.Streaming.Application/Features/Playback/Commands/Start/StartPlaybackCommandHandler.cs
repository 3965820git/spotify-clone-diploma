using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Application.Features.Playback.Queries;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;
using SpotifyClone.Streaming.Domain.ValueObjects;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.Start;

internal sealed class StartPlaybackCommandHandler(
    IStreamingUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<StartPlaybackCommand, PlaybackSessionDetails>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<PlaybackSessionDetails>> Handle(
        StartPlaybackCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Result.Failure<PlaybackSessionDetails>(PlaybackErrors.NotLoggedIn);
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
                PlaybackSessionId.New(), userId, deviceId, context, nowUtc, request.PositionMs, tracks.ToList());
        }
        else
        {
            session.StartNewPlayback(deviceId, context, nowUtc, request.PositionMs, tracks);
        }

        await _unit.PlaybackSessions.SaveAsync(session, cancellationToken);

        return new PlaybackSessionDetails(
            session.Id.Value,
            session.UserId.Value,
            session.TrackId.Value,
            session.DeviceId.Value,
            session.Context.Type,
            session.Context.ExternalId,
            session.CurrentPositionMs,
            session.IsPlaying,
            session.Shuffle,
            session.RepeatMode.ToString(),
            session.UpdatedAtUtc);
    }
}
