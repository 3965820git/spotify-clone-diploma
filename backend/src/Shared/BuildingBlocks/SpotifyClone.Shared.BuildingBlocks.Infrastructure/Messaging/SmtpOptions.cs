namespace SpotifyClone.Shared.BuildingBlocks.Infrastructure.Messaging;

public sealed record SmtpOptions
{
    public const string SectionName = "Smtp";

    public required string Host { get; init; }
    public required int Port { get; init; }
    public required bool EnableSsl { get; init; }
    public string? UserName { get; init; }
    public string? Password { get; init; }
    public string? DefaultFromEmail { get; init; }
    public string? DefaultFromName { get; init; }
}
