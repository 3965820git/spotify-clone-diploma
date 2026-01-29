namespace SpotifyClone.Shared.BuildingBlocks.Application.Errors;

public static class EmailErrors
{
    public static readonly Error SendFailed = new Error(
        "Email.SendFailed",
        "Failed to send email.");
}
