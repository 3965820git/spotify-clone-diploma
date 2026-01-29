namespace SpotifyClone.Accounts.Infrastructure.Auth.Jwt;

public sealed record JwtOptions
{
    public const string SectionName = "Jwt";

    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SecretKey { get; init; }
    public required int AccessTokenLifetimeMinutes { get; init; }
    public required int RefreshTokenLifetimeDays { get; init; }
}
