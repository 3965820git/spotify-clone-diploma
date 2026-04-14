using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Queries.GetProfileDetails;

public sealed class GetUserProfileDetailsQueryValidator
    : AbstractValidator<GetUserProfileDetailsQuery>
{
    public GetUserProfileDetailsQueryValidator()
        => RuleFor(x => x.UserId)
            .NotNull().WithMessage("User ID is required.")
            .NotEqual(Guid.Empty).WithMessage("User ID is required.");
}
