using SpotifyClone.Accounts.Application.Abstractions;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.Delete;

public sealed record DeleteUserCommand(
    Guid UserId)
    : IAccountsPersistentCommand<DeleteUserCommandResult>;
