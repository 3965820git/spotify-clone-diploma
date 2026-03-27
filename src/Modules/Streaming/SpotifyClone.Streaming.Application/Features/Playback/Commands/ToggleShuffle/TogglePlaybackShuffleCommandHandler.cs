using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;
using SpotifyClone.Streaming.Domain.ValueObjects;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.ToggleShuffle;

internal sealed class TogglePlaybackShuffleCommandHandler(
    IStreamingUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<TogglePlaybackShuffleCommand, TogglePlaybackShuffleCommandResult>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<TogglePlaybackShuffleCommandResult>> Handle(
        TogglePlaybackShuffleCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Result.Failure<TogglePlaybackShuffleCommandResult>(PlaybackErrors.NotLoggedIn);
        }

        PlaybackSession? session = await _unit.PlaybackSessions.GetByUserIdAsync(
            UserId.From(_currentUser.Id), cancellationToken);
        if (session is null)
        {
            return Result.Failure<TogglePlaybackShuffleCommandResult>(PlaybackErrors.SessionNotFound);
        }

        session.ToggleShuffle(DeviceId.From(request.DeviceId));

        await _unit.PlaybackSessions.SaveAsync(session, cancellationToken);

        return new TogglePlaybackShuffleCommandResult(session.IsShuffled);
    }
}
