using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Auth.Initialization;

public static class IdentitySeeder
{
    public static async Task SeedRolesAsync(IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();

        RoleManager<IdentityRole<Guid>> roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        string[] roles =
        {
            UserRoles.Listener,
            UserRoles.Creator,
            UserRoles.Admin
        };

        foreach (string role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }
}
