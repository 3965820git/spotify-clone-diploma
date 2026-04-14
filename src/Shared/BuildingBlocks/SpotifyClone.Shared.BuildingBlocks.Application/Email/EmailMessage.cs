namespace SpotifyClone.Shared.BuildingBlocks.Application.Email;

public sealed record EmailMessage(
    IEnumerable<string> To,
    string Subject,
    string? HtmlBody = null,
    string? PlainTextBody = null,
    (string Name, string Email)? From = null,
    IEnumerable<string>? Cc = null,
    IEnumerable<string>? Bcc = null,
    string? ReplyTo = null);
