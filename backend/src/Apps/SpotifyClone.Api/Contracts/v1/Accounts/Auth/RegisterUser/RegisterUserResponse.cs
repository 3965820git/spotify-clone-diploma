namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.RegisterUser;

public sealed record RegisterUserResponse(
    Guid UserId,
    string Email,
    string DisplayName,
    DateTimeOffset BirthDate,
    string Gender,
    string AccessToken);
