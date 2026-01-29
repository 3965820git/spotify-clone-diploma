namespace SpotifyClone.Shared.BuildingBlocks.Application.Configuration;

public sealed record ApplicationSettings
{
    public const string SectionName = "Application";
    public required string DomainName { get; init; }
}
