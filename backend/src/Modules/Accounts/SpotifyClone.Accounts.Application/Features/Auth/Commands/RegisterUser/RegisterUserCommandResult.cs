namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.RegisterUser;

public sealed record RegisterUserCommandResult(
    Guid UserId,
    string Email,
    string DisplayName,
    DateTimeOffset BirthDate,
    string Gender);
