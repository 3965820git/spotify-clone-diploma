using SpotifyClone.Accounts.Application.Abstractions;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.EditPersonalInfo;

public sealed record EditUserPersonalInfoCommand(
    Guid UserId,
    string Email,
    string? Password,
    string Gender,
    DateTimeOffset BirthDateUtc)
    : IAccountsPersistentCommand<EditUserPersonalInfoCommandResult>;
