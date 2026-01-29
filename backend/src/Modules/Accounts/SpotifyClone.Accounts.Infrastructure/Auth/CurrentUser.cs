using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Infrastructure.Auth;

internal sealed class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

    public UserId UserId
    {
        get
        {
            Claim? subClaim =
                (_httpContextAccessor.HttpContext?
                    .User?
                    .FindFirst(ClaimTypes.NameIdentifier))
                ?? throw new UnauthorizedAccessException("User is not authenticated.");

            return UserId.From(Guid.Parse(subClaim.Value));
        }
    }
}
