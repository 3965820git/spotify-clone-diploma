using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.RemoveTrackFromQueue;

internal sealed class RemoveTrackFromPlaybackQueueCommandHandler(
    IStreamingUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<RemoveTrackFromPlaybackQueueCommand, RemoveTrackFromPlaybackQueueCommandResult>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<RemoveTrackFromPlaybackQueueCommandResult>> Handle(
        RemoveTrackFromPlaybackQueueCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Result.Failure<RemoveTrackFromPlaybackQueueCommandResult>(PlaybackErrors.NotLoggedIn);
        }

        if (!_currentUser.IsPremium)
        {
            return Result.Failure<RemoveTrackFromPlaybackQueueCommandResult>(PlaybackErrors.NotAllowed);
        }

        PlaybackSession? session = await _unit.PlaybackSessions.GetByUserIdAsync(
            UserId.From(_currentUser.Id), cancellationToken);
        if (session is null)
        {
            return Result.Failure<RemoveTrackFromPlaybackQueueCommandResult>(PlaybackErrors.SessionNotFound);
        }

        session.RemoveTrackFromQueue(TrackId.From(request.TrackId));

        await _unit.PlaybackSessions.SaveAsync(session, cancellationToken);

        return new RemoveTrackFromPlaybackQueueCommandResult();
    }
}
