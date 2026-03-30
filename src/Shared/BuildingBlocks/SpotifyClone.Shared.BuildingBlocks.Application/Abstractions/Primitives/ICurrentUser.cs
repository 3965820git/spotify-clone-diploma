namespace SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;

public interface ICurrentUser
{
    Guid Id { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    bool IsInRole(string role);
}
