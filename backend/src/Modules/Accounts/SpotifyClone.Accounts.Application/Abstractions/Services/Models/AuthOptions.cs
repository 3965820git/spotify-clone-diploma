namespace SpotifyClone.Accounts.Application.Abstractions.Services.Models;

public sealed record AuthOptions
{
    public const string SectionName = "Auth";
    public required string FrontendUrl { get; init; }
}
