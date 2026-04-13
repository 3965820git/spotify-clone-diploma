using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.LinkToAudioFile;

internal sealed class LinkTrackToAudioFileCommandHandler(
    ICatalogUnitOfWork unit,
    ILogger<LinkTrackToAudioFileCommandHandler> logger)
    : ICommandHandler<LinkTrackToAudioFileCommand, LinkTrackToAudioFileCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ILogger<LinkTrackToAudioFileCommandHandler> _logger = logger;

    public async Task<Result<LinkTrackToAudioFileCommandResult>> Handle(
        LinkTrackToAudioFileCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Linking Track {TrackId} with Audio File {AudioFileId}", request.TrackId, request.AudioFileId);

        Track? track = await _unit.Tracks.GetByIdAsync(
            TrackId.From(request.TrackId),
            cancellationToken);

        if (track is null)
        {
            _logger.LogWarning(
                "Track {TrackId} not found while linking Track to Audio File", request.TrackId);

            return Result.Failure<LinkTrackToAudioFileCommandResult>(TrackErrors.NotFound);
        }

        track.LinkAudioFile(
            AudioFileId.From(request.AudioFileId),
            request.Duration);

        return new LinkTrackToAudioFileCommandResult();
    }
}
