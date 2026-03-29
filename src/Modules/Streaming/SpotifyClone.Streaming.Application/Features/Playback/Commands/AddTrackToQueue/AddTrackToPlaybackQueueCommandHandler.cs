using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.AddTrackToQueue;

internal sealed class AddTrackToPlaybackQueueCommandHandler(
    IStreamingUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<AddTrackToPlaybackQueueCommand, AddTrackToPlaybackQueueCommandResult>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<AddTrackToPlaybackQueueCommandResult>> Handle(
        AddTrackToPlaybackQueueCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Result.Failure<AddTrackToPlaybackQueueCommandResult>(PlaybackErrors.NotLoggedIn);
        }

        PlaybackSession? session = await _unit.PlaybackSessions.GetByUserIdAsync(
            UserId.From(_currentUser.Id), cancellationToken);
        if (session is null)
        {
            return Result.Failure<AddTrackToPlaybackQueueCommandResult>(PlaybackErrors.SessionNotFound);
        }

        session.AddTrackToQueue(TrackId.From(request.TrackId));

        await _unit.PlaybackSessions.SaveAsync(session, cancellationToken);

        return new AddTrackToPlaybackQueueCommandResult();
    }
}
