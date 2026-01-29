using SpotifyClone.Accounts.Application.Abstractions.Services.Models;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Abstractions.Services;

public interface ITokenService
{
    AccessToken GenerateAccessToken(
        IdentityUserInfo user,
        IReadOnlyCollection<string> roles,
        IReadOnlyDictionary<string, string>? claims = null);

    RefreshTokenEnvelope GenerateRefreshToken(UserId userId);
}
