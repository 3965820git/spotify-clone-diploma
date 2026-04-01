using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Models;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Infrastructure.Services.Jwt;

internal sealed class JwtTokenService(IOptions<JwtOptions> options)
    : ITokenService
{
    private readonly JwtOptions _options = options.Value;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    public AccessToken GenerateAccessToken(
        IdentityUserInfo user,
        IReadOnlyCollection<string> roles,
        IReadOnlyCollection<Claim>? claims = null)
    {
        DateTime now = DateTime.UtcNow;

        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(now).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64)
        };

        if (!string.IsNullOrEmpty(user.Email))
        {
            jwtClaims.Add(new(JwtRegisteredClaimNames.Email, user.Email));
            jwtClaims.Add(new(
                JwtRegisteredClaimNames.EmailVerified,
                user.EmailConfirmed.ToString().ToLowerInvariant()));
        }

        if (!string.IsNullOrEmpty(user.PhoneNumber))
        {
            jwtClaims.Add(new(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber));
            jwtClaims.Add(new(
                JwtRegisteredClaimNames.PhoneNumberVerified,
                user.PhoneNumberConfirmed.ToString().ToLowerInvariant()));
        }

        foreach (string role in roles)
        {
            jwtClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        if (claims is not null)
        {
            foreach (Claim claim in claims)
            {
                jwtClaims.Add(claim);
            }
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        DateTime expiresAt = DateTime.UtcNow.AddMinutes(_options.AccessTokenLifetimeMinutes);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: jwtClaims,
            notBefore: now,
            expires: expiresAt,
            signingCredentials: signingCredentials);

        string accessToken = _tokenHandler.WriteToken(token);

        return new AccessToken(accessToken, expiresAt);
    }

    public RefreshTokenEnvelope GenerateRefreshToken(UserId userId)
    {
        const int tokenLength = 32;
        byte[] randomBytes = new byte[tokenLength];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        string rawToken = Convert.ToHexString(randomBytes);
        DateTimeOffset expiresAt = DateTimeOffset.UtcNow
            .AddDays(_options.RefreshTokenLifetimeDays);

        return new RefreshTokenEnvelope(userId, rawToken, expiresAt, true);
    }
}
