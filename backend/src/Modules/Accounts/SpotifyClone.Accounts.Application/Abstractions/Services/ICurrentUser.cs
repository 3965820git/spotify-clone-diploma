using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Abstractions.Services;

public interface ICurrentUser
{
    UserId UserId { get; }
    bool IsAuthenticated { get; }
}
