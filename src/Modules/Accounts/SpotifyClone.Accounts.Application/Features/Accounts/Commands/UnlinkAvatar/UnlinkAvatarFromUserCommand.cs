using SpotifyClone.Accounts.Application.Abstractions;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.UnlinkAvatar;

public sealed record UnlinkAvatarFromUserCommand(
    Guid UserId)
    : IAccountsPersistentCommand<UnlinkAvatarFromUserCommandResult>;
