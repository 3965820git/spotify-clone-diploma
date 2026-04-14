using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Catalog.Domain.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.MarkAsReadyToPublish;

internal sealed class MarkTrackAsReadyToPublishCommandHandler(
    ICatalogUnitOfWork unit,
    ILogger<MarkTrackAsReadyToPublishCommandHandler> logger)
    : ICommandHandler<MarkTrackAsReadyToPublishCommand, MarkTrackAsReadyToPublishCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ILogger<MarkTrackAsReadyToPublishCommandHandler> _logger = logger;

    public async Task<Result<MarkTrackAsReadyToPublishCommandResult>> Handle(
        MarkTrackAsReadyToPublishCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Publishing Track {TrackId}", request.TrackId);

        Track? track = await _unit.Tracks.GetByIdAsync(
            TrackId.From(request.TrackId),
            cancellationToken);

        if (track is null)
        {
            _logger.LogWarning(
                "Track {TrackId} not found while publishing", request.TrackId);

            return Result.Failure<MarkTrackAsReadyToPublishCommandResult>(TrackErrors.NotFound);
        }

        track.MarkAsReadyToPublish();

        return new MarkTrackAsReadyToPublishCommandResult();
    }
}
