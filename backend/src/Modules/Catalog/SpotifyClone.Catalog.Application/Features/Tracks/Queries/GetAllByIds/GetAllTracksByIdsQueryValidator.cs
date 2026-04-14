using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries.GetAllByIds;

public sealed class GetAllTracksByIdsQueryValidator
    : AbstractValidator<GetAllTracksByIdsQuery>
{
    public GetAllTracksByIdsQueryValidator()
        => RuleFor(x => x.TrackIds)
            .NotEmpty().WithMessage("Track IDs are required.");
}
