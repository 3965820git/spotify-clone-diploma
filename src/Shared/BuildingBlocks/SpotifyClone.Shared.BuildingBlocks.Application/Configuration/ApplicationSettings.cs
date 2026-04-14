namespace SpotifyClone.Shared.BuildingBlocks.Application.Configuration;

public sealed record ApplicationSettings
{
    public const string SectionName = "Application";
    public required string DomainName { get; init; }
    public required string FrontendUrl { get; init; }
    public required string ApiUrl { get; init; }
    public required string MediaStorageUrl { get; init; }
}
