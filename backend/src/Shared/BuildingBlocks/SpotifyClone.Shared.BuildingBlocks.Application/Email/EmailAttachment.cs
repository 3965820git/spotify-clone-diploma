namespace SpotifyClone.Shared.BuildingBlocks.Application.Email;

public sealed record EmailAttachment(string FileName, Stream Content, string ContentType);
