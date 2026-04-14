namespace SpotifyClone.Api.Contracts.v1.Accounts.Profile.EditPersonalInfo;

public sealed record EditUserPersonalInfoRequest
{
    public required string Email { get; init; }
    public string? Password { get; init; }
    public required string Gender { get; init; }
    public required DateTimeOffset BirthDateUtc { get; init; } // 2007-04-25T17:07:17
}
