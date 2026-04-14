using SpotifyClone.Accounts.Application.Abstractions;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.EditProfileDetails;

public sealed record EditUserProfileDetailsCommand(
    Guid UserId,
    string DisplayName)
    : IAccountsPersistentCommand<EditUserProfileDetailsCommandResult>;
