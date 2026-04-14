using MediatR;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Features.Tracks.Commands.MarkAsReadyToPublish;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Jobs;

public sealed class MarkTrackAsReadyToPublishJob(
    ISender sender,
    ICatalogUnitOfWork unit)
{
    private readonly ISender _sender = sender;
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task ProcessAsync(
        Guid trackId,
        CancellationToken cancellationToken = default)
    {
        Result<MarkTrackAsReadyToPublishCommandResult> result = await _sender.Send(
            new MarkTrackAsReadyToPublishCommand(trackId),
            cancellationToken);

        if (result.IsFailure)
        {
            string errorMessage = string.Join("\n", result.Errors.Select(e => $"{e.Code}: {e.Description}"));

            throw new InvalidOperationException(
                $"MarkTrackAsReadyToPublishJob failed for Track {trackId}. Errors:\n{errorMessage}");
        }

        await _unit.CommitAsync(cancellationToken);
    }
}
