using System.Security.Claims;
using SpotifyClone.Accounts.Application.Models;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Abstractions.Services;

public interface ITokenService
{
    AccessToken GenerateAccessToken(
        IdentityUserInfo user,
        IReadOnlyCollection<string> roles,
        IReadOnlyCollection<Claim>? claims = null);

    RefreshTokenEnvelope GenerateRefreshToken(UserId userId);
}
