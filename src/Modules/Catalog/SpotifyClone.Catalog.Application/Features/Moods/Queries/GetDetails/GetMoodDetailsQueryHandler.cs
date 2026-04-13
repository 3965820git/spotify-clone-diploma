using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions.Data;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Moods.Queries.GetDetails;

internal sealed class GetMoodDetailsQueryHandler(
    IMoodReadService moodReadService,
    ILogger<GetMoodDetailsQueryHandler> logger)
    : IQueryHandler<GetMoodDetailsQuery, MoodDetails>
{
    private readonly IMoodReadService _moodReadService = moodReadService;
    private readonly ILogger<GetMoodDetailsQueryHandler> _logger = logger;

    public async Task<Result<MoodDetails>> Handle(
        GetMoodDetailsQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting Mood info {MoodId}", request.MoodId);

        MoodDetails? mood = await _moodReadService.GetDetailsAsync(
            MoodId.From(request.MoodId),
            cancellationToken);

        if (mood is null)
        {
            _logger.LogWarning(
                "Mood {MoodId} not found", request.MoodId);

            return Result.Failure<MoodDetails>(MoodErrors.NotFound);
        }

        return mood;
    }
}
