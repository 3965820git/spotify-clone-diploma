using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Moods.Queries.GetDetails;

public sealed class GetMoodDetailsQueryValidator
    : AbstractValidator<GetMoodDetailsQuery>
{
    public GetMoodDetailsQueryValidator()
        => RuleFor(x => x.MoodId)
        .NotNull().WithMessage("Mood ID is required.")
        .NotEqual(Guid.Empty).WithMessage("Mood ID is required.");
}
