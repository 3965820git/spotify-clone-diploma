using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries.GetSummary;

public sealed class GetTrackSummaryQueryValidator
    : AbstractValidator<GetTrackSummaryQuery>
{
    public GetTrackSummaryQueryValidator()
        => RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
}
