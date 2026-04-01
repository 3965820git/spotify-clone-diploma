using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Infrastructure.Auth;

internal sealed class CurrentUser(
    IHttpContextAccessor httpContextAccessor)
    : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    private ClaimsPrincipal? User =>
        _httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated == true;

    public Guid Id
    {
        get
        {
            Claim? subClaim =
                User?.FindFirst(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User is not authenticated.");

            return Guid.Parse(subClaim.Value);
        }
    }

    public string? Email
    {
        get
        {
            ArgumentNullException.ThrowIfNull(User);
            return User.FindFirst(ClaimTypes.Email)?.Value;
        }
    }

    public bool IsPremium
    {
        get
        {
            ArgumentNullException.ThrowIfNull(User);
            return User.HasClaim(c => c.Type == "subscription_level" && c.Value == "premium");
        }
    }

    public bool IsInRole(string role)
    {
        if (User is null || !IsAuthenticated)
        {
            return false;
        }

        return User.IsInRole(role);
    }
}
