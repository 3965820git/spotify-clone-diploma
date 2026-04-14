using SpotifyClone.Accounts.Application.Abstractions;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string Password,
    string DisplayName,
    DateTimeOffset BirthDate,
    string Gender,
    string Role)
    : IAccountsPersistentCommand<RegisterUserCommandResult>;
