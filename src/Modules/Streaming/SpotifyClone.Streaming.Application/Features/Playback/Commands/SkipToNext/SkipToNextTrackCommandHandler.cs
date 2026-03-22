using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;
using SpotifyClone.Streaming.Domain.ValueObjects;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SkipToNext;

internal sealed class SkipToNextTrackCommandHandler(
    IStreamingUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<SkipToNextTrackCommand, SkipToNextTrackCommandResult>
{
    private readonly IStreamingUnitOfWork _unit = unit;
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

        return new SkipToNextTrackCommandResult();
    }
}
