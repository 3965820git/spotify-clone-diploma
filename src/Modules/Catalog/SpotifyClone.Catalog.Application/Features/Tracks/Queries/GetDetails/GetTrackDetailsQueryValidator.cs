using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries.GetDetails;

public sealed class GetTrackDetailsQueryValidator
    : AbstractValidator<GetTrackDetailsQuery>
{
    public GetTrackDetailsQueryValidator()
        => RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
}
