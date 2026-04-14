using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.Delete;

public sealed class DeleteUserCommandValidator
    : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
        => RuleFor(x => x.UserId)
            .NotNull().WithMessage("User ID is required.")
            .NotEqual(Guid.Empty).WithMessage("User ID is required.");
}
